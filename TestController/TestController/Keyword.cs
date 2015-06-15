using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestController
{
    public class Keyword
    {
        public string KeywordName { get; set; }
        public List<KeywordStep> Step = new List<KeywordStep>();
    }
}
