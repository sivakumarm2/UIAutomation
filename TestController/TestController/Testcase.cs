using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestController
{
    public class Testcase
    {
        public string TestCaseId { get; set; }
        public string TestCaseDesc { get; set; }
        public string Keyword { get; set; }
        public string KeywordFile { get; set; }
        public string KeywordSheet { get; set; }
        public string TestDataFile { get; set; }
        public string TestDataSheet { get; set; }
        public string TestDataSetId { get; set; }
        public List<Testdata> TestDataSet = new List<Testdata>();
        public string Status { get; set; }
        public string OnFailure { get; set; }
        public string RecoveryStep { get; set; }

    }
}
