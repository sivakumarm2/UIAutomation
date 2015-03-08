using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Collections.Generic;
using UI.Automation.Win.ObjectRepository;
using UI.Automation.Web.ObjectRepository;


namespace UI.Automation.Win
{
    /// <summary>
    /// Summary description for TeamPad
    /// </summary>
    [CodedUITest]
    public class UITestController
    {
        public static string testCaseID = string.Empty;
        private string _sxlFile = string.Empty;
        private string sTxtFilePath = string.Empty;
        private string sErrorLogs = string.Empty;
        private bool bXlopened = default(bool);

        Process cmdp = new Process();

        private long lRows = default(long);
        private long lCols = default(long);
        private string strClass = string.Empty;
        private string sTestcaseId = string.Empty;
        private string sTcId = string.Empty;
        private string sTctemp = string.Empty;
        private string strKeyword = string.Empty;
        private string strParam = string.Empty;
        private long lCurrRow = default(long);
        private bool bRuNextKeyword = true;
        private string sStartTime = string.Empty;
        private string sTestcaseDesc = string.Empty;
        Hashtable ht_ExcelProcess = default(Hashtable);

        //Added for Testing DBAutomation 
        public SqlConnection TestConn = default(SqlConnection);
        public static DataSet dsTestCase = new DataSet();
        public static string sTestCaseDescChk = string.Empty;
        public static string sTestCaseIdChk = string.Empty;
        public string ScrShot = string.Empty;
        int ItestExec = 0;
       
        private StreamReader sr = default(StreamReader);
        private StreamWriter sw = default(StreamWriter);

        public UITestController()
        {
            sTxtFilePath = @"F:\UIAutomation\Logs.txt";

        }

        public string sTResult = string.Empty;
        private string[] csv_lines = default(string[]);
        private string sTestSetPath = string.Empty;
        private int csv_Lines_Index = default(int);
        private string file_Type = string.Empty;
        private int iRowIndexTemp = default(int);
        string xmloutput = string.Empty;
        private bool LastKeywordIndex = default(bool);
        private string FailureLogPath = string.Empty;
        private bool xmlFileExits = default(bool);
        private static System.Timers.Timer aTimer = default(System.Timers.Timer);
        System.Threading.Timer Timer = default(System.Threading.Timer);
        System.DateTime StopTime = default(System.DateTime);
        static int counter = default(int);
        public static string currentPickingTripId = string.Empty;


        [TestMethod, Timeout(999999999)]
        public void RunTestSet()
        {
            //if (CommonVariables.EnableCoverage.ToString() == "1")
            //{
            //    if (CommonVariables.EnableInstrumentation == "1")
            //    {
            //        //to delete all .exe and .orig files from existing folders befour copying
            //        //  PDBFileDelete();

            //        CopyPdbFiles();
            //        StartInstrumentation();
            //    }
            //    StartCoverage(); // If you dont want to collect the coverage report, please comment this line  and as well as stopCoverage at the end of loop.
            //}
            //else if (CommonVariables.EnableProfiling.ToString() == "1")
            //{
            //    StartProfiling();
            //}

            if (Directory.Exists(CommonVariables.ResultXmlPath + DateTime.Now.ToString("dd-MM-yyyy")))
            {
                CommonVariables.ResultXmlPath = CommonVariables.ResultXmlPath + DateTime.Now.ToString("dd-MM-yyyy") + @"\";
            }
            else
            {
                Directory.CreateDirectory(CommonVariables.ResultXmlPath + DateTime.Now.ToString("dd-MM-yyyy"));
                CommonVariables.ResultXmlPath = CommonVariables.ResultXmlPath + DateTime.Now.ToString("dd-MM-yyyy") + @"\";
            }
            //if (File.Exists(CommonVariables.ResultXmlPath + CommonVariables.ResultXmlName))
            //{
            //    xmlFileExits = true;
            //    OldXmlFile.Load(CommonVariables.ResultXmlPath + CommonVariables.ResultXmlName);
            //}

            DirectoryInfo dir = new DirectoryInfo(CommonVariables.TestCasePath);

                  ReadXLOrCSVFiles(dir);
          
            //if (CommonVariables.EnableProfiling.ToString() == "1" || CommonVariables.EnableCoverage.ToString() == "1")
            //{
            //    StopCoverage();
            //}
        }

        public void ReadXLOrCSVFiles(DirectoryInfo dir)
        {
            int i = 1;
            foreach (FileInfo fs in dir.GetFiles())
            {
                if (fs.Name.ToUpper() == "SET" + i + ".XLSX" || fs.Name.ToUpper() == "SET" + i + ".XLS")
                {
                    LastKeywordIndex = false;
                    sTestSetPath = fs.FullName;
                    CommonVariables.PresentCscFilePath = fs.FullName;
                    _sxlFile = sTestSetPath;
                    i = i + 1;
                    file_Type = "XLS";
                }
                else if (fs.Name.ToUpper() == "SET" + i + ".CSV")
                {
                    LastKeywordIndex = false;
                    sTestSetPath = fs.FullName;
                    CommonVariables.PresentCscFilePath = fs.FullName;
                    _sxlFile = sTestSetPath;
                    i = i + 1;
                    file_Type = "CSV";
                    //SetCsvKeywords(sTestSetPath); //Old Code

                    if (CommonVariables.SanityCheck)
                        SetCsvKeywords(sTestSetPath);
                    else
                        break;
                }
            }
        }

        [TestMethod]
        public void SetCsvKeywords(string strTestSetPath)
        {
            try
            {

                csv_lines = File.ReadAllLines(strTestSetPath);
               
                for (int i = 0; i < csv_lines.GetLength(0); i++)
                {
                    string[] row;
                    csv_Lines_Index = i;
                    row = csv_lines[i].Split(',');
                    if (row[0].ToString().Trim() != "")
                    {
                        sTestcaseId = row[0].ToString().Trim();
                        testCaseID = sTestcaseId;
                    }
                    else
                        sTestcaseId = null;
                    if (row[1].ToString().Trim() != "")
                        sTestcaseDesc = row[1].ToString().Trim();
                    else
                        sTestcaseDesc = "";
                    if (row[2].ToString().Trim() != "")
                        strKeyword = row[2].ToString().Trim().ToUpper();
                    else
                        strKeyword = null;
                    if (row[3].ToString().Trim() != "")
                        strParam = row[3].ToString().Trim();
                    else
                        strParam = null;
                    iRowIndexTemp = i + 1;
                    if (iRowIndexTemp == csv_lines.GetLength(0))
                        LastKeywordIndex = true;

                    ExecuteKeywords();
                }
            }

            catch (Exception e)
            {
                CommonVariables.sComExceptionlog = e.Message.ToString();
                Playback.PlaybackSettings.ContinueOnError = true;
                WriteResultLogs(false);
                bXlopened = false;
            }
        }

        public void ExecuteKeywords()
        {
            try
            {

                //string sKeywordType;
                CommonVariables.sComExceptionlog = "";
                string testcaseStatus = "Pass";
                //StartCoverage(); // If you dont want to collect the coverage report, please comment this line  and as well as stopCoverage at the end of loop.
                strKeyword = strKeyword.Trim();
                if (sTestcaseId != null)
                {
                    sTcId = sTestcaseId;
                    sTctemp = sTestcaseId;
                    bRuNextKeyword = true;
                    WriteErrLog(sTxtFilePath, "Running " + sTestcaseId + " " + DateTime.Now.ToString());

                    //if (iRowIndexTemp == 1)
                    //{
                    //    _testCase = Factory.GetTestCase;
                    //    _testCase.Id = sTcId;
                    //    _testCase.StartTime = DateTime.Now.ToString();
                    //}
                    //if (iRowIndexTemp != 1)
                    //{
                    //    for (int j = 0; j < _testCase.KeyWords.Count; j++)
                    //    {
                    //        if (_testCase.KeyWords[j].Status == "Fail")
                    //        {
                    //            testcaseStatus = "Fail";
                    //            break;
                    //        }
                    //    }
                    //    _testCase = Factory.GetTestCase;
                    //    _testCase.Id = sTcId;
                    //    _testCase.StartTime = DateTime.Now.ToString();
                    //}
                }
                else
                {
                    sTcId = sTctemp;
                }
                sStartTime = DateTime.Now.ToString("hh:mm:s");


                //Eliminated switch case statement for invoking methods dynamically.
                if (bRuNextKeyword)
                {
                    bool isMethodInvoked = ExecuteDynamicKeywords(strKeyword, strParam);
                    WriteResultLogs(isMethodInvoked);

                }

                //if (sTestcaseId != null)
                //{
                //    _testCase.Description = sTestcaseDesc; //Add Description here
                //    _testResult.TestCases.Add(_testCase);
                //    int count = _testResult.TestCases.Count;
                //    if (count == 1)
                //    {
                //        _testResult.TestCases[0].Result = "Inprogress";
                //    }
                //    else
                //    {
                //        int PrvCount = count - 1;   //Eliminating Latest testcase
                //        for (int i = 0; i < PrvCount; i++)
                //        {
                //            for (int j = 0; j < _testResult.TestCases[i].KeyWords.Count; j++)
                //            {
                //                if (_testResult.TestCases[i].KeyWords[j].Status == "Fail")
                //                {
                //                    testcaseStatus = "Fail";
                //                    break;
                //                }
                //                else
                //                    testcaseStatus = "Pass";
                //            }
                //            _testResult.TestCases[i].Result = testcaseStatus;
                //        }
                //        _testResult.TestCases[count - 1].Result = "Inprogress";
                //    }
                //}
                //if (LastKeywordIndex)
                //{
                //    for (int j = 0; j < _testCase.KeyWords.Count; j++)
                //    {
                //        if (_testCase.KeyWords[j].Status == "Fail")
                //        {
                //            testcaseStatus = "Fail";
                //            break;
                //        }
                //        else
                //            testcaseStatus = "Pass";
                //    }
                //    _testCase.Result = testcaseStatus;
                //}
                //if (xmlFileExits == true)
                //{
                //    XmlDocument x2 = new XmlDocument();
                //    xmloutput = GenericXmlSerializer.SerializeObj(_testResult);
                //    x2.LoadXml(xmloutput);
                //    string strOldXml = OldXmlFile.LastChild.LastChild.InnerXml;
                //    string strNewXml = ((x2.LastChild).LastChild).InnerXml;
                //    string strFinalXml = strOldXml + strNewXml;
                //    string rootName = OldXmlFile.DocumentElement.Name.ToString();
                //    string FirstsubNode = OldXmlFile.DocumentElement.FirstChild.Name.ToString();
                //    xmloutput = "<" + rootName + ">" + "<" + FirstsubNode + ">" + strFinalXml + "</" + FirstsubNode + ">" + "</" + rootName + ">";
                //    resultXml.LoadXml(xmloutput);
                //    resultXml.Save(CommonVariables.ResultXmlPath + CommonVariables.ResultXmlName);
                //}
                //else
                //{
                //    xmloutput = GenericXmlSerializer.SerializeObj(_testResult);
                //    resultXml.LoadXml(xmloutput);
                //    resultXml.Save(CommonVariables.ResultXmlPath + CommonVariables.ResultXmlName);
                //}
                //StopCoverage();

            }

            catch (Exception e)
            {
                CommonVariables.sComExceptionlog = e.Message.ToString();
                Playback.PlaybackSettings.ContinueOnError = true;
                WriteResultLogs(false);
                bXlopened = false;
            }

        }

        private bool ExecuteDynamicKeywords(string keywordName, string parameterName)
        {
            bool status = default(bool);

            try
            {
                string @namespace = CommonVariables.AutomationNameSpace;

                var executingAssembly = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                         where t.IsClass && t.Namespace == @namespace
                                         select t).ToList();

                var y = executingAssembly[0].GetMethods();
                if (executingAssembly != default(List<Type>) && executingAssembly.Count > 0)
                {
                    foreach (var eAssemblyType in executingAssembly)
                    {
                        Type currentType = eAssemblyType.UnderlyingSystemType;


                        MethodInfo methodInfo = currentType.GetMethod(keywordName);
                        if (methodInfo != default(MethodInfo))
                        {
                            object classInstance = Activator.CreateInstance(currentType, null);

                            if (classInstance != default(object))
                            {
                                if (!string.IsNullOrEmpty(parameterName))
                                {
                                    status = Convert.ToBoolean(methodInfo.Invoke(classInstance, new object[] { parameterName }));
                                    break;
                                }
                                else
                                {
                                    status = Convert.ToBoolean(methodInfo.Invoke(classInstance, null));
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                status = false;
                CommonVariables.sComExceptionlog = ex.Message.ToString();
            }
            return status;
        }


        public void WriteResultLogs(bool bResult)
        {
            string sResult;
            string sFailLog;
            //bResult = true;
            string sEndTime = DateTime.Now.ToString("hh:mm:ss");
            ScrShot = "";
            if (bResult)
            {
                sResult = "Pass"; FailureLogPath = "";
            }
            else
            {
                sResult = "Fail";
                bRuNextKeyword = false;
                CaptureScreen();
            }

            if (CommonVariables.TestType == "1")
            {
                //Call DB
                //dbUtil.CaptureTestResults("1",
                //                          testCaseID,
                //                          strKeyword,
                //                          sResult,
                //                          CommonVariables.sComExceptionlog,
                //                          ((Convert.ToDateTime(sEndTime).Subtract(Convert.ToDateTime(sStartTime)).ToString())).ToString(),
                //                          ScrShot);
            }
            else
            {
                if (file_Type == "XLS")
                {
                    //m_objSheet.Cells[lCurrRow, 5] = sResult; //Logs status of the keyword Fail / Pass
                    //m_objSheet.Cells[lCurrRow, 6] = CommonVariables.sComExceptionlog; //Logs any Playback exceptions
                    //m_objSheet.Cells[lCurrRow, 7] = CommonVariables.sComExceptionlog; //Logs any generic exceptions
                    //m_objSheet.Cells[lCurrRow, 8] = Convert.ToDateTime(sEndTime).Subtract(Convert.ToDateTime(sStartTime)).ToString();
                }
                else
                {
                    string RowVal = csv_lines[csv_Lines_Index].ToString();
                    string[] Split_row = csv_lines[csv_Lines_Index].Split(',');
                    if (Split_row.Length > 4)
                    {
                        int Split_row_leg = Split_row[0].Length + Split_row[1].Length + Split_row[2].Length + Split_row[3].Length + 3;  //3 for commas in string line
                        RowVal = RowVal.Remove(Split_row_leg) + "," + sResult + "," + CommonVariables.sComExceptionlog.Replace("\n", "").Replace("\r", "") + "," + "" + "," + Convert.ToDateTime(sEndTime).Subtract(Convert.ToDateTime(sStartTime)).ToString();
                    }
                    else
                        RowVal += "," + sResult + "," + CommonVariables.sComExceptionlog.Replace("\n", "").Replace("\r", "") + "," + "" + "," + Convert.ToDateTime(sEndTime).Subtract(Convert.ToDateTime(sStartTime)).ToString();
                    csv_lines[csv_Lines_Index] = RowVal;
                    File.WriteAllLines(sTestSetPath, csv_lines);
                }
                File.WriteAllLines(sTestSetPath, csv_lines);
            }
           
            Playback.PlaybackSettings.ContinueOnError = true;
        }

      
        
        public void ClearDownPickingList()
        {
            string[] filePaths = Directory.GetFiles(CommonVariables.ClearDownPickingList, "*.xml");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        public void CaptureScreen()
        {
            string sFileName = sTcId + "_" + strKeyword + "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + DateTime.Now.ToString("hh-mm-ss");
            Image failureScrn = UITestControl.Desktop.CaptureImage();
            FailureLogPath = sFileName + ".png";
            //failureScrn.Save(CommonVariables.CaptureScreen + sFileName + ".png");
            failureScrn.Save(CommonVariables.ResultXmlPath + sFileName + ".png");
            ScrShot = CommonVariables.ResultXmlPath + sFileName + ".png";
        }
        [TestMethod]
        public void InvokeClearDownMethods()
        {
            CaptureScreen();
            ClearDownPickingList();
        }

        //[TestMethod]
        //public void StartCoverage()
        //{
        //    string batchPath = "C:\\CoverageStart.bat";
        //    string sCoverageFileName = "Applications_" + DateTime.Now.ToString("dd-MM-yyyy") + ".coverage";
        //    string CoveragePath = CommonVariables.CoverageReports;
        //    StreamWriter sw = new StreamWriter(batchPath);
        //    string sParam = CommonVariables.coveragepathVS2010;
        //    string driveName = sParam.Remove(1).ToString();
        //    sw.WriteLine(driveName + ":");
        //    sw.WriteLine("cd " + sParam);

        //    string StartCoverage = "start vsperfmon /CS /user:everyone /coverage /output:";
        //    sw.WriteLine(StartCoverage.ToString() + CoveragePath + sCoverageFileName);
        //    sw.Close();
        //    Process p = Process.Start(batchPath);
        //    p.WaitForExit();
        //    // File.Delete(batchPath);

        //    ServiceController[] svcs = ServiceController.GetServices();

        //    var windowsServiceList = (from service in svcs
        //                              where service.DisplayName.StartsWith("GHS")
        //                              select new
        //                              {
        //                                  DisplayName = service.ServiceName,
        //                                  Status = service.Status.ToString()
        //                              }
        //                             ).ToList();
        //    for (int i = 0; i <= windowsServiceList.Count - 1; i++)
        //    {
        //        ServiceController ghs = new ServiceController();
        //        TimeSpan timeout = TimeSpan.FromMilliseconds(60000);
        //        ghs.ServiceName = windowsServiceList[i].DisplayName;
        //        if (ghs.Status == ServiceControllerStatus.Running)
        //        {
        //            ghs.Stop();
        //            ghs.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
        //            ghs.Start();
        //            ghs.WaitForStatus(ServiceControllerStatus.Running, timeout);
        //        }
        //        else
        //        {
        //            ghs.Start();
        //            ghs.WaitForStatus(ServiceControllerStatus.Running, timeout);
        //        }
        //    }
        //}

        //[TestMethod]
        //public void StopCoverage()
        //{
        //    string batchPath = "C:\\CoverageStop.bat";
        //    string CoveragePath = CommonVariables.CoverageReports;

        //    StreamWriter sw = new StreamWriter(batchPath);
        //    string sParam = CommonVariables.coveragepathVS2010;
        //    string driveName = sParam.Remove(1).ToString();
        //    sw.WriteLine(driveName + ":");
        //    sw.WriteLine("cd " + sParam);
        //    sw.WriteLine("iisreset");
        //    sw.WriteLine("vsperfcmd /shutdown");
        //    sw.WriteLine("VSPerfCLREnv /off");
        //    sw.WriteLine("Success");
        //    sw.Close();

        //    ServiceController[] svcs = ServiceController.GetServices();

        //    var windowsServiceList = (from service in svcs
        //                              where service.DisplayName.StartsWith("GHS")
        //                              select new
        //                              {
        //                                  DisplayName = service.ServiceName,
        //                                  Status = service.Status.ToString()
        //                              }
        //                             ).ToList();
        //    for (int i = 0; i <= windowsServiceList.Count - 1; i++)
        //    {
        //        ServiceController ghs = new ServiceController();
        //        TimeSpan timeout = TimeSpan.FromMilliseconds(60000);
        //        ghs.ServiceName = windowsServiceList[i].DisplayName;
        //        if (ghs.Status == ServiceControllerStatus.Running)
        //        {
        //            ghs.Stop();
        //            ghs.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
        //        }
        //    }

        //    Process p = Process.Start(batchPath);
        //    p.WaitForExit();
        //    // File.Delete(batchPath);
        //}

        //[TestMethod]
        //public void StartProfiling()
        //{
        //    string sProcess = CommonVariables.CoveragePath;
        //    string sParam = CommonVariables.coveragepathVS2010;
        //    string sProfileFileName = "MPA." + DateTime.Now.ToString("dd-MM-yyyy") + "--" + DateTime.Now.ToString("hh-mm-ss") + ".vsp";

        //    cmdp.StartInfo.RedirectStandardInput = true;
        //    cmdp.StartInfo.RedirectStandardOutput = true;
        //    cmdp.StartInfo.UseShellExecute = false;

        //    cmdp.StartInfo.FileName = sProcess;
        //    cmdp.Start();

        //    System.IO.StreamReader sOut = cmdp.StandardOutput;
        //    StreamWriter myStreamWriter = cmdp.StandardInput;

        //    string driveName = sParam.Remove(1).ToString();
        //    myStreamWriter.WriteLine(driveName + ":");
        //    myStreamWriter.WriteLine("cd " + sParam);

        //    if (CommonVariables.ProfilingType.ToUpper() == "EXE")
        //    {
        //        myStreamWriter.WriteLine("VSPerfCLREnv /SampleOn");
        //        myStreamWriter.WriteLine("VSPerfCLREnv /InteractionOn");
        //        myStreamWriter.WriteLine(CommonVariables.ProfileReports + sProfileFileName);
        //    }
        //    else if (CommonVariables.ProfilingType.ToUpper() == "WEB")
        //    {
        //        myStreamWriter.WriteLine(CommonVariables.ProfileWebAppReports + sProfileFileName);
        //    }

        //    // cmdp.Close();
        //    Thread.Sleep(10000);
        //}

        public void WriteErrLog(string sfilePath, string serrlog)
        {
            FileStream fs = null;
            if (!File.Exists(sfilePath))
            {
                using (fs = File.Create(sfilePath))
                {

                }
                using (StreamWriter sw = new StreamWriter(sfilePath))
                {
                    sw.WriteLine(serrlog);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(sfilePath, true))
                {
                    sw.WriteLine(serrlog);
                }
            }

        }


        //@################################################################################################@
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //RunTestSet();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {

            //KillExcel();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        }


        public bool UA_AddNo()
        {
            try
            {

                //Please add all your valid code in this block
                int sum = 10 + 10;
                return true;

            }
            catch (PlaybackFailureException playbackExp)
            {
                string sPlaybackExceptionLog = playbackExp.Message; //sPlaybackExceptionLog declare this variable in Variable map
                return false;
            }

            catch (Exception e)
            {
                string sComExceptionLog = e.Message; //sComExceptionLog declare this variable in Variable map
                return false;
            }
        }

        // #endregion
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private TestContext testContextInstance;

    }
}


