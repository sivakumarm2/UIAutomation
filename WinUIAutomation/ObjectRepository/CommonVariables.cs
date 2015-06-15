using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace UI.Automation.Win.ObjectRepository
{
    public static class CommonVariables
    {
        static CommonVariables()
        {
            CommonVariables.ApplictionUnderTest = ConfigurationManager.AppSettings["ApplictionUnderTest"];
            CommonVariables.TestCasePath = ConfigurationManager.AppSettings["TestCasePath"];
            CommonVariables.ClearDownPickingList = ConfigurationManager.AppSettings["ClearDownPickingList"];
            CommonVariables.CaptureScreen = ConfigurationManager.AppSettings["CaptureScreen"];
            CommonVariables.sqlscripts = ConfigurationManager.AppSettings["sqlscripts"];
            CommonVariables.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            CommonVariables.AppExe = ConfigurationManager.AppSettings["AppExe"];
            CommonVariables.CoveragePath = ConfigurationManager.AppSettings["CoveragePath"];
            CommonVariables.coveragepathVS2010 = ConfigurationManager.AppSettings["coveragepathVS2010"];
            CommonVariables.Logspath = ConfigurationManager.AppSettings["Logspath"];
            CommonVariables.CoverageReports = ConfigurationManager.AppSettings["CoverageReports"];
            CommonVariables.ResultXmlPath = ConfigurationManager.AppSettings["ResultXmlPath"];
            CommonVariables.ResultXmlName = ConfigurationManager.AppSettings["ResultXmlName"];
            //Added for TestCase Automation
            CommonVariables.TestConnectionString = ConfigurationManager.AppSettings["TestConnectionString"];

            CommonVariables.sTxtFilePath = CommonVariables.Logspath;
            string NoOfOrders = ConfigurationManager.AppSettings["NoOfOrders"];


            CommonVariables.AutomationNameSpace = ConfigurationManager.AppSettings["AutomationNameSpace"];

        }

        public static string ApplictionUnderTest = "Dcos";
        public static string TestCasePath;
        public static string ClearDownPickingList;
        public static string CaptureScreen;
        public static string sqlscripts;
        public static string ConnectionString;
        public static string AppExe;
        public static string CoveragePath;
        public static string coveragepathVS2010;
        public static string Logspath;
        public static string CoverageReports;
        public static string ResultXmlPath;
        public static string ResultXmlName;
        //Added for Testing DBAutomation 
        public static string TestConnectionString;
        /*---------------------------------------*/

        public static string AutomationNameSpace;
        public static string KeywordClass;
        public static string sTxtFilePath;

        public static string TestType;
        public static string sComExceptionlog;

    }
}
