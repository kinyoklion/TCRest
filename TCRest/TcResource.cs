using System;
using System.Collections.Generic;
using System.Text;

namespace TCRest
{
	public class TcResource
	{
		public string ResourcePath {private set; get;}

		private List<TcMethod> methods = new List<TcMethod>();

		public TcResource(string resourcePath)
		{
			ResourcePath = resourcePath;
		}

		public void AddMethod(TcMethod method)
		{
			methods.Add(method);
		}

	    public override string ToString()
	    {
	        var builder = new StringBuilder();
	        builder.Append("Path: ");
	        builder.AppendLine(ResourcePath);
	        if(methods.Count > 0)
	        {
	            builder.AppendLine("Methods:");
	        }
	        foreach(var method in methods)
	        {
	            builder.Append("\t");
	            builder.AppendLine(method.MethodId);
	        }
	        return builder.ToString();
	    }
	}
}

