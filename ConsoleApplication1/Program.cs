using ConsoleApplication1ry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UI.Automation.Win;
using UI.Automation.ObjectUtility;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            
            p.GenerateExcelData();

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
                
                lsNamespace = namespaces.Split('@').ToList();
                foreach (string @namespace in lsNamespace)
                {

                    var executingAssembly = (from t in Assembly.Load(@namespace).GetTypes()
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
                }
            }

            catch (Exception ex)
            {
                status = false;
                CommonVariables.sComExceptionlog = ex.Message.ToString();
            }
            return status;
        }

        public void GenerateExcelData()
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
                string Keyword = "";
                string DataSheet = "";
                string Dataset = "";
                string TargetKeyword = "";
                string TargetParam = "";

                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM " + "[" + sSheet + "$]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(dsTestcase);

                string c = dsTestcase.Tables[0].Rows[0][0].ToString();
                for (int i = 0; i < dsTestcase.Tables[0].Rows.Count; i++)
                {
                    KeywordSheet = dsTestcase.Tables[0].Rows[i]["KeywordSheet"].ToString();
                    Keyword = dsTestcase.Tables[0].Rows[i]["Keyword"].ToString();
                    DataSheet = dsTestcase.Tables[0].Rows[i]["DataSheet"].ToString();
                    Dataset = dsTestcase.Tables[0].Rows[i]["Dataset"].ToString();

                    //GetKeywords
                    if (Keyword.Contains("CK_"))
                    {
                        cmd.CommandText = "SELECT * FROM " + "[" + KeywordSheet + "$] WHERE Keyword=" + "'" + Keyword + "'";
                        oleda = new OleDbDataAdapter(cmd);
                        oleda.Fill(dsKeyword);
                        int cnt = dsKeyword.Tables[0].Rows.Count;
                        string k = dsKeyword.Tables[0].Rows[0][0].ToString();

                        //Get Destdata
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

                        for(int iTestdataIte=0;iTestdataIte<dsTestdata.Tables[0].Rows.Count;iTestdataIte++)
                        {
                            for(int iKeywordstepIte=0;iKeywordstepIte<dsKeyword.Tables[0].Rows.Count;iKeywordstepIte++)
                            {
                                string step="";
                                string objectpath="";
                                string Refvalue = "";
                                string pValue = "";

                                step = dsKeyword.Tables[0].Rows[iKeywordstepIte]["Steps"].ToString();
                                objectpath = dsKeyword.Tables[0].Rows[iKeywordstepIte]["ObjectPath"].ToString();
                                Refvalue = dsKeyword.Tables[0].Rows[iKeywordstepIte]["Value"].ToString();
                                TargetKeyword = step;
                                if(Refvalue !="")                                                                       
                                {
                                    pValue = dsTestdata.Tables[0].Rows[iTestdataIte][Refvalue].ToString();
                                    TargetParam = objectpath + "," + pValue;
                                }
                                else
                                {
                                    TargetParam = objectpath;

                                }
                                ExecuteDynamicKeywords(TargetKeyword, TargetParam);

                            }

                        }

                    }

                    else
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
                        for(int iTestdataRow=0;iTestdataRow<dsTestdata.Tables[0].Rows.Count;iTestdataRow++)
                        {
                            string inParam = "";
                            for (int iTestDataCol = 1; iTestDataCol < dsTestdata.Tables[0].Columns.Count; iTestDataCol++)
                            {                                
                                if (iTestDataCol != 1)
                                {
                                    inParam =inParam +","+ dsTestdata.Tables[0].Rows[iTestdataRow][iTestDataCol].ToString();
                                }

                                else
                                {
                                    inParam =dsTestdata.Tables[0].Rows[iTestdataRow][iTestDataCol].ToString();
                                }
                            }

                            TargetParam = inParam;
                            ExecuteDynamicKeywords(TargetKeyword, TargetParam);
                        }

                    }
                    dsTestdata.Tables[0].Clear();

                }
                
            }

            
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
               
            }


        }


    }

}
