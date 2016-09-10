using System;
using System.Net;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using TCRest.Parsers;

namespace TCRest
{
	public class TcServer
	{
		private Uri server;
		private CredentialCache credentialCache;
		private bool loggedIn;
	    private IList<TcResource> appResources; 

		#region Constants

	    private const string HttpAuth = "httpAuth";
	    private const string RestApp = HttpAuth + "/app/rest";
		private const string ApplicationLocator = RestApp + "/application.wadl";
	    private const string ProjectsPath = RestApp + "/projects";

	    #endregion

		public TcServer(Uri serverUri)
		{
			server = serverUri;
		}

		public bool Login(string userName, string password)
		{
			//Must log out before logging in.
			if(loggedIn)
			{
				return false;
			}

		    var networkCredential = new NetworkCredential(userName, password);
			var tempCredentialCache = new CredentialCache {{server, "Basic", networkCredential}};

		    try
			{
				var webRequest = WebRequest.Create(server + ApplicationLocator);
				webRequest.Credentials = tempCredentialCache;
				var response = webRequest.GetResponse();

				using(var stream = response.GetResponseStream())
				using(var reader = new StreamReader(stream))
				{
					appResources = ResourceParser.ParseApplication(reader.ReadToEnd());
				}

				//Assign the credentials to use for future requests.
				credentialCache = tempCredentialCache;
				loggedIn = true;
				return true;
			}
			catch(WebException we)
			{
			    if(we.Response == null) throw;
			    if(((HttpWebResponse) we.Response).StatusCode != HttpStatusCode.Unauthorized) throw;

			    Console.WriteLine("Invalid user/password.");
			    return false;
			}
		}

        public void Logout()
        {
            if (!loggedIn)
            {
                return;
            }
            server = null;
            credentialCache = null;
            loggedIn = false;
        }

	    public IList<TcProject> GetProjects()
	    {
	        var requestUrl = server + ProjectsPath;

            var response = GetUrl(requestUrl);
	        var projects = ProjectParser.Parse(response);
	        return projects;
	    }

	    public IList<TcBuild> GetBuilds(string buildTypeId)
	    {
	        var requestUrl = server + RestApp + "/buildTypes/id:" + buildTypeId + "/builds/";
	        var response = GetUrl(requestUrl);
	        var builds = BuildParser.Parse(response);
            return builds;
	    }

	    public IList<TcBuildType> GetBuildTypes(string projectId)
	    {
	        var request = server + ProjectsPath + "/id:" + projectId + "/buildTypes";
	        var response = GetUrl(request);
	        var buildTypes = BuildTypeParser.Parse(response);
	        return buildTypes;
	    }

	    public string DoCommand(string command)
	    {
	        var requestUrl = server + HttpAuth + command;
	        return GetUrl(requestUrl);
	    }

	    private string GetUrl(string requestUrl)
	    {
            CheckLoggedIn();

	        var webRequest = WebRequest.Create(requestUrl);
	        webRequest.Credentials = credentialCache;
	        var response = webRequest.GetResponse();

	        using(var stream = response.GetResponseStream())
	        using(var reader = new StreamReader(stream))
	        {
	            return reader.ReadToEnd();
	        }
	    }

	    public IList<string> GetResources()
	    {
            CheckLoggedIn();
	        return appResources.Select(appResource => appResource.ResourcePath).ToList();
	    }

	    public TcResource GetResource(string path)
	    {
            CheckLoggedIn();
	        return appResources.FirstOrDefault(resource => resource.ResourcePath == path);
	    }

	    private void CheckLoggedIn()
	    {
	        if (!loggedIn)
	        {
	            throw new InvalidOperationException("Call not allowed unless logged in.");
	        }
	    }
	}
}

