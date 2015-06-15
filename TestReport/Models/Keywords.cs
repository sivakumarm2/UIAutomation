using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestReport.Models
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Keywords
    {

        private List<Keyword> keywordField;

        /// <remarks/>
        public List<Keyword> Keyword
        {
            get
            {
                return this.keywordField;
            }
            set
            {
                this.keywordField = value;
            }
        }
    }

}
