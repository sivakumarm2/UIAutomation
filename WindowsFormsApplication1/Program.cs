using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AddControlType());
        }
    }


    public class TestConfiguration
    {
        public List<TestAction> actions = new List<TestAction>();


    }

    public class TestAction
    {
        public string ActionKeyword { get; set; }
        public List<TestStep> steps = new List<TestStep>();


    }

    public class TestStep
    {
        public int StepNo { get; set; }
        public string StepName { get; set; }
        public string control  { get; set; }

    }

    public class ControlRepository
    {

        int _nextId=0;
        public List<ControlInfo> controls = new List<ControlInfo>();
        public int contorlNextId
        {
            get
            {
                _nextId++;
                return _nextId;
            }
            set { _nextId = value; }
        }
    }

    public class ControlInfo
    {

        public string window { get; set; }
        public string  ControlType  { get; set; }     
        public string controlName { get; set; }
        public string controlParent { get; set; }
        public int controlId { get; set; }
        public int parentId { get; set; }


    }

    public class WindowRepository
    {
        public List<Window> window = new List<Window>();

    }

    public class ControlTypeRepo
    {
        public List<ControlType> controltype = new List<ControlType>();
    }

    public class Window
    {
        public string Name { get; set; }
       
    }

    public class ControlType
    {
        public string Type { get; set; }

    }
     

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
