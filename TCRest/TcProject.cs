using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCRest
{
    public class TcProject
    {
        public string Id { private set; get; }
        public string Name { private set; get; }

        public string Description { private set; get; }

        public string Href { private set; get; }

        public string WebUrl { private set; get; }

        public string ParentProjectId { private set; get; }

        public TcProject(string id, string name, string description, string href, string webUrl, string parentProjectId)
        {
            Id = id;
            Name = name;
            Description = description;
            Href = href;
            WebUrl = webUrl;
            ParentProjectId = parentProjectId;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);

            builder.Append("\t");
            builder.AppendLine(Id);

            builder.Append("\t");
            builder.AppendLine(Description);

            builder.Append("\t");
            builder.AppendLine(Href);

            builder.Append("\t");
            builder.AppendLine(WebUrl);

            builder.Append("\t");
            builder.AppendLine(ParentProjectId);

            return builder.ToString();
        }
    }
}
