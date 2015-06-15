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
using UI.Automation.Win;
using UI.Automation.ObjectUtility;
using System.Data.OleDb;





namespace UI.Automation.Win
{
    /// <summary>
    /// Summary description for TeamPad
    /// </summary>
    [CodedUITest]
    public class UITestController
    {
        public static string testCaseID = string.Empty;
        public static string testCaseDesc = string.Empty;
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
        //DXControlExtension dxControl = new DXControlExtension();

        TestResults Results = new TestResults();
        TestCase Testcase = new TestCase();
        Keyword keyword = new Keyword();

        private string sPath;// Path to stroe test reults and scrren shots.
        private string sResultDirectory;

        public UITestController()
        {
            sTxtFilePath = @"F:\Github\UIAutomation\Logs.txt";
            Playback.PlaybackSettings.SmartMatchOptions = SmartMatchOptions.None;

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
        public void RUNTEST()
        {            
            ExecuteTestCases();     
          
        }
     
        public void ExecuteTestCases()
        {
            try
            {
                string path = @"F:\Temp\edit\QAAutomationData.xlsx";
                var oledbConn = new OleDbConnection();

                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. */

                string sSheet = "Testcase";

                if (Path.GetExtension(path) == ".xls")
                {
                    oledbConn =
                        new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path +
                                            ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(path) == ".xlsx")
                {
                    oledbConn =
                        new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                            ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }

                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet dsTestcase = new DataSet();
                DataSet dsKeyword = new DataSet();
                DataSet dsTestdata = new DataSet();


                string KeywordSheet = "";
                string TestCaseId = "";
                string Keyword = "";
                string DataSheet = "";
                string Dataset = "";
                string TargetKeyword = "";
                string TargetParam = "";
                bool stepPass = true;
                bool keywordPass = true;
                bool runNextKeyword = true;
                string status = "Pass";

                int TotalRowCount;

                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM " + "[" + sSheet + "$]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(dsTestcase);
                

                TotalRowCount = Convert.ToInt32(dsTestcase.Tables[0].Rows.Count);

                string c = dsTestcase.Tables[0].Rows[0][0].ToString();
                for (int i = 0; i < dsTestcase.Tables[0].Rows.Count; i++)
                {


                    stepPass = true;
                    keywordPass = true;
                    status = "Pass";

                    KeywordSheet = dsTestcase.Tables[0].Rows[i]["KeywordSheet"].ToString();
                    testCaseID = dsTestcase.Tables[0].Rows[i]["TestCaseId"].ToString();
                    Keyword = dsTestcase.Tables[0].Rows[i]["Keyword"].ToString();
                    DataSheet = dsTestcase.Tables[0].Rows[i]["DataSheet"].ToString();
                    Dataset = dsTestcase.Tables[0].Rows[i]["Dataset"].ToString();
                    testCaseDesc = dsTestcase.Tables[0].Rows[i]["TestCase Desc"].ToString();

                    strKeyword = Keyword;

                    if (testCaseID != "")
                    {
                        Testcase = new TestCase();
                        keyword = new Keyword();

                        Testcase.Id = testCaseID;
                        Testcase.Descriptioin = testCaseDesc;
                        sTcId = testCaseID;
                        runNextKeyword = true;

                    }

                    if (runNextKeyword)
                    {
                        //GetKeywords
                        if (Keyword.Contains("CK_"))
                        {
                            cmd.CommandText = "SELECT * FROM " + "[" + KeywordSheet + "$] WHERE Keyword=" + "'" + Keyword + "'";
                            oleda = new OleDbDataAdapter(cmd);
                            oleda.Fill(dsKeyword);
                            int cnt = dsKeyword.Tables[0].Rows.Count;
                            string k = dsKeyword.Tables[0].Rows[0][0].ToString();


                            //Get Destdata
                            if (Dataset != "")
                            {
                                if (Dataset == "All")
                                {
                                    cmd.CommandText = "SELECT * FROM " + "[" + DataSheet + "$]";
                                }
                                else
                                {
                                    cmd.CommandText = "SELECT * FROM " + "[" + DataSheet + "$] WHERE Dataset=" + "'" + Dataset + "'";
                                }
                                oleda = new OleDbDataAdapter(cmd);
                                oleda.Fill(dsTestdata);
                                int cnt1 = dsTestdata.Tables[0].Rows.Count;
                                string k1 = dsTestdata.Tables[0].Rows[0][0].ToString();

                                for (int iTestdataIte = 0; iTestdataIte < dsTestdata.Tables[0].Rows.Count; iTestdataIte++)
                                {
                                    for (int iKeywordstepIte = 0; iKeywordstepIte < dsKeyword.Tables[0].Rows.Count; iKeywordstepIte++)
                                    {
                                        string step = "";
                                        string objectpath = "";
                                        string Refvalue = "";
                                        string pValue = "";

                                        step = dsKeyword.Tables[0].Rows[iKeywordstepIte]["Steps"].ToString();
                                        objectpath = dsKeyword.Tables[0].Rows[iKeywordstepIte]["ObjectPath"].ToString();
                                        Refvalue = dsKeyword.Tables[0].Rows[iKeywordstepIte]["Value"].ToString();
                                        TargetKeyword = step;
                                        if (Refvalue != "")
                                        {
                                            pValue = dsTestdata.Tables[0].Rows[iTestdataIte][Refvalue].ToString();
                                            TargetParam = objectpath + "," + pValue;
                                        }
                                        else
                                        {
                                            TargetParam = objectpath;

                                        }
                                        CommonVariables.AutomationNameSpace = "UI.Automation.ObjectUtility";
                                        CommonVariables.KeywordClass = "ActionHandlerForClassicKeyword";
                                        stepPass = ExecuteDynamicKeywords(TargetKeyword, TargetParam);
                                        if (!stepPass)
                                        {
                                            runNextKeyword = false;
                                            keywordPass = false;
                                            status = "Fail";

                                            Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });

                                            Testcase.Status = "Fail";
                                            Results.TestCases.Add(Testcase);
                                            InvokeClearDownMethods();
                                            break;
                                        }                                       

                                    }

                                    Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });
                                    if (!stepPass) break;

                                }


                                dsTestdata.Tables[0].Rows.Clear();
                                dsTestdata.Tables[0].Columns.Clear();
                            }
                            else  // keywords that have no parameters
                            {
                                for (int iKeywordstepIte = 0; iKeywordstepIte < dsKeyword.Tables[0].Rows.Count; iKeywordstepIte++)
                                {
                                    string step = "";
                                    string objectpath = "";
                                    string Refvalue = "";
                                    string pValue = "";

                                    step = dsKeyword.Tables[0].Rows[iKeywordstepIte]["Steps"].ToString();
                                    objectpath = dsKeyword.Tables[0].Rows[iKeywordstepIte]["ObjectPath"].ToString();
                                    Refvalue = dsKeyword.Tables[0].Rows[iKeywordstepIte]["Value"].ToString();
                                    TargetKeyword = step;

                                    TargetParam = objectpath;

                                    CommonVariables.AutomationNameSpace = "UI.Automation.ObjectUtility";
                                    CommonVariables.KeywordClass = "ActionHandlerForClassicKeyword";
                                    stepPass = ExecuteDynamicKeywords(TargetKeyword, TargetParam);
                                    if (!stepPass)
                                    {
                                        runNextKeyword = false;
                                        keywordPass = false;
                                        status = "Fail";

                                        Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });

                                        Testcase.Status = "Fail";
                                        Results.TestCases.Add(Testcase);
                                        InvokeClearDownMethods();
                                        break;
                                    }                                    

                                }
                                Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });
                           
                            }

                            dsKeyword.Tables[0].Rows.Clear();
                            dsKeyword.Tables[0].Columns.Clear();
                        }



                        else // Business keyword Path
                        {
                            if (Dataset != "")
                            {

                                TargetKeyword = Keyword;
                                //Get Testdata
                                if (Dataset == "All")
                                {
                                    cmd.CommandText = "SELECT * FROM " + "[" + DataSheet + "$]";
                                }
                                else
                                {
                                    cmd.CommandText = "SELECT * FROM " + "[" + DataSheet + "$] WHERE Dataset=" + "'" + Dataset + "'";
                                }

                                oleda = new OleDbDataAdapter(cmd);
                                oleda.Fill(dsTestdata);
                                int cnt1 = dsTestdata.Tables[0].Rows.Count;
                                string k1 = dsTestdata.Tables[0].Rows[0][0].ToString();
                                for (int iTestdataRow = 0; iTestdataRow < dsTestdata.Tables[0].Rows.Count; iTestdataRow++)
                                {
                                    string inParam = "";
                                    for (int iTestDataCol = 1; iTestDataCol < dsTestdata.Tables[0].Columns.Count; iTestDataCol++)
                                    {
                                        if (iTestDataCol != 1)
                                        {
                                            inParam = inParam + "," + dsTestdata.Tables[0].Rows[iTestdataRow][iTestDataCol].ToString();
                                        }

                                        else
                                        {
                                            inParam = dsTestdata.Tables[0].Rows[iTestdataRow][iTestDataCol].ToString();
                                        }
                                    }

                                    TargetParam = inParam;
                                    CommonVariables.AutomationNameSpace = "UI.Automation.Win";
                                    CommonVariables.KeywordClass = "";
                                    stepPass = ExecuteDynamicKeywords(TargetKeyword, TargetParam);

                                    if (!stepPass)
                                    {
                                        runNextKeyword = false;
                                        keywordPass = false;
                                        status = "Fail";

                                        Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });

                                        Testcase.Status = "Fail";
                                        Results.TestCases.Add(Testcase);
                                        InvokeClearDownMethods();
                                        break;
                                    }

                                }

                                Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });

                                dsTestdata.Tables[0].Rows.Clear();
                                dsTestdata.Tables[0].Columns.Clear();
                                
                            }
                            else //Business keywords that have no parameters
                            {
                                TargetKeyword = Keyword;
                                TargetParam = "";
                                CommonVariables.AutomationNameSpace = "UI.Automation.Win";
                                CommonVariables.KeywordClass = "";
                                stepPass = ExecuteDynamicKeywords(TargetKeyword, TargetParam);

                                if (!stepPass)
                                {
                                    runNextKeyword = false;
                                    keywordPass = false;
                                    status = "Fail";

                                    Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });

                                    Testcase.Status = "Fail";
                                    Results.TestCases.Add(Testcase);
                                    InvokeClearDownMethods();
                                }

                                Testcase.Keywords.Add(new Keyword { Name = Keyword, Status = status });

                            }

                        }

                        if (keywordPass)
                        {
                            testCaseID = dsTestcase.Tables[0].Rows[i + 1]["TestCaseId"].ToString();
                            if (testCaseID != "")
                            {

                                Testcase.Status = "Pass";
                                Results.TestCases.Add(Testcase);
                                SaveTestResults();
                            }
                        }
                    }

               }                

               
                oledbConn.Dispose();
            }
                
           

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
                
            }


        }
        
        private bool ExecuteDynamicKeywords(string keywordName, string parameterName)
        {
            
            bool status = default(bool);

            try
            {   
                List<string> namespacelist = new List<string>();
                List<string> classlist = new List<string>();

                List<string> lsNamespace = new List<string>();
                string namespaces = CommonVariables.AutomationNameSpace;
                string strClass = CommonVariables.KeywordClass;
                lsNamespace = namespaces.Split('@').ToList();
                foreach (string @namespace in lsNamespace)
                {

                    var executingAssembly = (from t in Assembly.Load(@namespace).GetTypes()
                                             where t.IsClass && t.Namespace == @namespace
                                             select t).ToList();

                    if (executingAssembly != default(List<Type>) && executingAssembly.Count > 0)
                    {
                        foreach (var eAssemblyType in executingAssembly)
                        {
                            Type currentType = eAssemblyType.UnderlyingSystemType;
                            
                            if(strClass!="")
                            {
                                if (currentType.FullName.Contains(strClass))
                                {

                                    MethodInfo methodInfo = currentType.GetMethod(keywordName);
                                    if (methodInfo != default(MethodInfo))
                                    {
                                        object classInstance = Activator.CreateInstance(currentType, null);


                                        if (classInstance != default(object))
                                        {
                                            if (!string.IsNullOrEmpty(parameterName))
                                            {
                                                string[] parameters = parameterName.Split(',');
                                                object[] parametersArray = new object[parameters.Length];
                                                for (int i = 0; i < parameters.Length; i++)
                                                {
                                                    parametersArray[i] = parameters[i].ToString();
                                                }
                                                status = Convert.ToBoolean(methodInfo.Invoke(classInstance, parametersArray));
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

                                else
                                {
                                    MethodInfo methodInfo = currentType.GetMethod(keywordName);
                                    if (methodInfo != default(MethodInfo))
                                    {
                                        object classInstance = Activator.CreateInstance(currentType, null);                               

                                        if (classInstance != default(object))
                                        {
                                            if (!string.IsNullOrEmpty(parameterName))
                                            {
                                                string[] parameters =  parameterName.Split(',');
                                                object[] parametersArray = new object[parameters.Length];
                                                for (int i = 0; i < parameters.Length; i++)
                                                {
                                                    parametersArray[i] = parameters[i].ToString();
                                                }
                                                status = Convert.ToBoolean(methodInfo.Invoke(classInstance,  parametersArray));
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
                }
            }

            catch (Exception ex)
            {
                status = false;
                CommonVariables.sComExceptionlog = ex.Message.ToString();
            }
            return status;
        }


        private void SaveTestResults()
        {
            try
            {
             
                string sFileName = "Results.xml";
                
                var text = Helper.ConvertObjectToXML(Results);
                sPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                sResultDirectory = DateTime.Now.ToString("dd-MM-yyyy");
                sPath = sPath + "\\" + sResultDirectory;           
                
                if (!System.IO.Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }

                System.IO.File.WriteAllText(sPath + "\\" + sFileName, text);
            }

            catch
            {

            }
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
                        int Split_row_leg = Split_row[0].Length + Split_row[1].Length + Split_row[2].Length + Split_row[3].Length + Split_row[4].Length + 4;  //4 for commas in string line
                        RowVal = RowVal.Remove(Split_row_leg) +","+  sResult + "," + CommonVariables.sComExceptionlog.Replace("\n", "").Replace("\r", "") + "," + Convert.ToDateTime(sEndTime).Subtract(Convert.ToDateTime(sStartTime)).ToString();
                    }
                    else
                        RowVal +=  ","+sResult + "," + CommonVariables.sComExceptionlog.Replace("\n", "").Replace("\r", "") + ","  + Convert.ToDateTime(sEndTime).Subtract(Convert.ToDateTime(sStartTime)).ToString();
                    csv_lines[csv_Lines_Index] = RowVal;
                    File.WriteAllLines(sTestSetPath, csv_lines);
                }
                File.WriteAllLines(sTestSetPath, csv_lines);
            }
           
            Playback.PlaybackSettings.ContinueOnError = true;
        }
        
        
        public void CaptureScreen()
        {
            string sFileName = sTcId + "_" + strKeyword + "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + DateTime.Now.ToString("hh-mm-ss");
            Image failureScrn = UITestControl.Desktop.CaptureImage();
            failureScrn.Save(sPath + "\\" + sFileName +".png");
        }
       
        public void InvokeClearDownMethods()
        {
            SaveTestResults();
            CaptureScreen();
            CloseApplication();
         
        }



        public void CloseApplication()
        {
            Thread.Sleep(10000);
            string processName = "Entrada.Editor";
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                process.Kill();
            }           
        }
        
        
        
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


