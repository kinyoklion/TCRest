using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TCRest
{
    static class BuildTypeParser
    {
        private const string BuildTypesNode = "buildTypes";
        private const string BuildTypeNode = "buildType";

        public static IList<TcBuildType> Parse(string buildTypesXml)
        {
            var xmlDocument = XDocument.Parse(buildTypesXml);
            var buildTypesNode = xmlDocument.Element(BuildTypesNode);
            var buildTypeList = buildTypesNode.Descendants(BuildTypeNode);
            return (from project in buildTypeList
                    let id = ParserUtility.GetOptionalAttribute(project, "id")
                    let name = ParserUtility.GetOptionalAttribute(project, "name")
                    let projectName = ParserUtility.GetOptionalAttribute(project, "projectName")
                    let projectId = ParserUtility.GetOptionalAttribute(project, "projectId")
                    let href = ParserUtility.GetOptionalAttribute(project, "href")
                    let webUrl = ParserUtility.GetOptionalAttribute(project, "webUrl")
                    select new TcBuildType(id, name, projectName, projectId, href, webUrl)).ToList();
        }
    }
}
