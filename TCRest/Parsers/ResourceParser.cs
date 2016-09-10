using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TCRest.Parsers
{
    static class ResourceParser
    {
        //TODO: Namespace should be programmatically accessed from the XML.
        private const string TcNamespace = "{http://wadl.dev.java.net/2009/02}";
        private const string ApplicationNode = TcNamespace + "application";
        private const string ResourcesNode = TcNamespace + "resources";
        private const string ResourceNode = TcNamespace + "resource";
        private const string MethodNode = TcNamespace + "method";

        public static IList<TcResource> ParseApplication(string applicationWadl)
        {
            var resources = new List<TcResource>();
            var xmlDocument = XDocument.Parse(applicationWadl);
            var applicationNode = xmlDocument.Element(ApplicationNode);
            var resourcesNode = applicationNode.Element(ResourcesNode);
            ProcessResources(resources, resourcesNode, "");
            return resources;
        }

        private static void ProcessResources(IList<TcResource> resources, XElement resourceNode, string rootResource)
        {
            foreach(var resource in resourceNode.Descendants(ResourceNode))
            {
                var resourcePath = rootResource + resource.Attribute("path").Value;

                var tcResource = new TcResource(resourcePath);
                resources.Add(tcResource);

                var methods = resource.Descendants(MethodNode);

                foreach(var method in methods)
                {
                    var methodName = method.Attribute("id").Value;
                    var tcMethod = new TcMethod(methodName);

                    tcResource.AddMethod(tcMethod);
                }

                var children = resource.Descendants(ResourceNode);
                if(children.Any())
                {
                    ProcessResources(resources, resource, resourcePath);
                }
            }
        }
    }
}
