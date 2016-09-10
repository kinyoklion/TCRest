using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCRest
{
    public class TcBuild
    {
        public string Id { private set; get; }
        public string BuildTypeId { private set; get; }

        public int Number { private set; get; }

        public string Status { private set; get; }

        public string State { private set; get; }

        public string Href { private set; get; }

        public string WebUrl { private set; get; }

        public TcBuild(string id, string buildTypeId, string number, string status, string state, string href, string webUrl)
        {
            Id = id;
            BuildTypeId = buildTypeId;
            Number = int.Parse(number);
            Status = status;
            State = state;
            Href = href;
            WebUrl = webUrl;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(BuildTypeId);
            builder.Append("<");
            builder.Append(Id);
            builder.Append(":");
            builder.Append(Number);
            builder.AppendLine(">");

            builder.Append("\t");
            builder.Append(State);

            builder.Append("\t");
            builder.Append(Status);

            builder.Append("\t");
            builder.Append(Href);

            builder.Append("\t");
            builder.Append(WebUrl);

            return builder.ToString();
        }
    }
}
