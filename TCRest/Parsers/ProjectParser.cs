using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TCRest
{
    internal static class ProjectParser
    {
        private const string ProjectsNode = "projects";
        private const string ProjectNode = "project";

        public static IList<TcProject> Parse(string projectsXml)
        {
            var xmlDocument = XDocument.Parse(projectsXml);
            var projectsNode = xmlDocument.Element(ProjectsNode);
            var projectList = projectsNode.Descendants(ProjectNode);
            return (from project in projectList
                let id = ParserUtility.GetOptionalAttribute(project, "id")
                let name = ParserUtility.GetOptionalAttribute(project, "name")
                let description = ParserUtility.GetOptionalAttribute(project, "description")
                let href = ParserUtility.GetOptionalAttribute(project, "href")
                let webUrl = ParserUtility.GetOptionalAttribute(project, "webUrl")
                let parentProjectId = ParserUtility.GetOptionalAttribute(project, "parentProjectId")
                select new TcProject(id, name, description, href, webUrl, parentProjectId)).ToList();
        }
    }
}
