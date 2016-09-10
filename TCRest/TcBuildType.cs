using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace TCRest
{
    public class TcBuildType
    {
        public string Id { private set; get; }

        public string Name { private set; get; }

        public string ProjectName { private set; get; }

        public string ProjectId { private set; get; }

        public string Href { private set; get; }

        public string WebUrl { private set; get; }

        public TcBuildType(string id, string name, string projectName, string projectId, string href, string webUrl)
        {
            Id = id;
            Name = name;
            ProjectName = projectName;
            ProjectId = projectId;
            Href = href;
            WebUrl = webUrl;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(Name);
            builder.Append(":");
            builder.AppendLine(Id);

            builder.Append("\t");
            builder.AppendLine(ProjectName);

            builder.Append("\t");
            builder.AppendLine(ProjectId);

            builder.Append("\t");
            builder.AppendLine(Href);

            builder.Append("\t");
            builder.AppendLine(WebUrl);

            return builder.ToString();
        }
    }
}
