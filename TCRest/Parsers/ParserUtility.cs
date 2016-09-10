using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TCRest
{
    public class ParserUtility
    {
        public static string GetOptionalAttribute(XElement element, string attributeName)
        {
            var attribute = element.Attribute(attributeName);
            if(attribute != null)
            {
                return attribute.Value;
            }
            return "None";
        }
    }
}
