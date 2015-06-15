using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestReport.Models
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public  class TestResults
    {

        private List<TestCase> testCasesField;

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