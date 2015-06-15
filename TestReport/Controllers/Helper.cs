using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace TestReport.Models
{
    public class Helper
    {
        public static string ConvertObjectToXML<T>(T preferences)
        {
            string Ret = "";

            XmlSerializer oXmlSerializer = new XmlSerializer(preferences.GetType());
            StringWriter Output = new StringWriter(new StringBuilder());

            oXmlSerializer.Serialize(Output, preferences);

            Ret = Output.ToString();

            return Ret;
        }
        public static T ConvertXMLDataToEntity<T>(string xmlData)
        {
            var oStringReader = new StringReader(xmlData);
            var oXmlSerializer = new XmlSerializer(typeof(T));
            var oXmlTextReader = new XmlTextReader(oStringReader);

            var entity = (T)oXmlSerializer.Deserialize(oXmlTextReader);

            oXmlTextReader.Close();
            oStringReader.Close();

            return entity;
        }
    }

}











