using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Automation.Win
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public  class TestResults
    {

        private List<TestCase> testCasesField;

        public TestResults()
        {
            TestCases = new List<TestCase>();
            //testCasesField = new List<TestCase>();
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TestCase", IsNullable = false)]
        public List<TestCase> TestCases
        {
            get
            {
                return this.testCasesField;
            }
            set
            {
                this.testCasesField = value;
            }
        }
    }

}