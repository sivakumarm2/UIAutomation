using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TestReport.Models;

namespace TestReport.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        TestResults testResult = new TestResults();
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            LoadTestResult();

            return View(testResult);
        }

        public ActionResult About()
        {
            return View();
        }
        public void LoadTestResult()
        {

            if (System.IO.File.Exists(@"F:\Edit1\TestReport\TestReport\TestResults.xml"))
            {
                var TResults = System.IO.File.ReadAllText(@"F:\Edit1\TestReport\TestReport\TestResults.xml");
                testResult = TestReport.Models.Helper.ConvertXMLDataToEntity<TestResults>(TResults);

            }

        }

    }
}
