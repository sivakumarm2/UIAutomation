using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestReport.Models
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public  class TestCase
    {

        private string idField;

        private string descriptioinField;

        private string statusField;

        private List<Keyword> keywordsField;

        /// <remarks/>
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string Descriptioin
        {
            get
            {
                return this.descriptioinField;
            }
            set
            {
                this.descriptioinField = value;
            }
        }

        /// <remarks/>
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public List<Keyword> Keywords
        {
            get
            {
                return this.keywordsField;
            }
            set
            {
                this.keywordsField = value;
            }
        }
    }

}