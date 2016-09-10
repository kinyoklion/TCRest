using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TCRest
{
    internal static class BuildParser
    {
        private const string BuildsNode = "builds";
        private const string BuildNode = "build";

        public static IList<TcBuild> Parse(string buildsXml)
        {
            var xmlDocument = XDocument.Parse(buildsXml);
            var buildsNode = xmlDocument.Element(BuildsNode);
            var buildList = buildsNode.Descendants(BuildNode);
            return (from project in buildList
                    let id = ParserUtility.GetOptionalAttribute(project, "id")
                    let buildTypeId = ParserUtility.GetOptionalAttribute(project, "buildTypeId")
                    let number =  ParserUtility.GetOptionalAttribute(project, "number")
                    let status =  ParserUtility.GetOptionalAttribute(project, "status")
                    let state = ParserUtility.GetOptionalAttribute(project, "state")
                    let href = ParserUtility.GetOptionalAttribute(project, "href")
                    let webUrl = ParserUtility.GetOptionalAttribute(project, "webUrl")
                    select new TcBuild(id, buildTypeId, number, status, state, href, webUrl)).ToList();
        }
    }
}
