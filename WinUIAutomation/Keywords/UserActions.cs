using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UI.Automation.ObjectUtility;
using Microsoft.VisualStudio.TestTools.UITesting;
using System.Threading;
using System.Diagnostics;



namespace UI.Automation.Win
{
    public class UserActions
    {
        

        public  UserActions()
        {
            Util.AUTPath = @"C:\Users\sivak_000\Desktop\Entrada Editor";
 
                       
        }

        #region login

       public bool DelayPlayback()
        {
            Thread.Sleep(120000);
            return true;
        }

       public bool LaunchApplication()
       {
           //ApplicationUnderTest.Launch(@"C:\Users\sivak_000\AppData\Local\Apps\2.0\KMK5TMWV.EKK\K9NC3KZZ.DA9\entr..tion_24a158b8b8b3d2d1_0001.0000_156d6952debb763b\Entrada.Editor.exe");
           ApplicationUnderTest.Launch(@"C:\Users\sivak_000\AppData\Local\Apps\2.0\KMK5TMWV.EKK\K9NC3KZZ.DA9\entr..tion_24a158b8b8b3d2d1_0001.0000_8dfc1e56e779b3b8\Entrada.Editor.exe");
                                         
           Thread.Sleep(10000);
           return true;
       }

        public bool CloseApplication()
       {
           Thread.Sleep(10000);
           string processName = "Entrada.Editor";
           Process[] processes = Process.GetProcessesByName(processName);
           foreach (Process process in processes)
           {
               process.Kill();
           }
           return true;
       }

        public bool ApplyHotKeys(string hotkey)             
        {
            Thread.Sleep(20000);
            var searchCriteria = new SearchCriteria { window = ObjectRepository.Window.Edit1HomePag};
            var actionHandler = new ActionHandler(searchCriteria);
            actionHandler.ApplyHotKey(hotkey);
            return true;
        
        }

        public bool Spellcheck()
        {
            bool foo = true;

            var searchCriteria1 = new SearchCriteria { window = ObjectRepository.Window.SpellingPopup, subWindow = ObjectRepository.Window.SpellingPopupSubWin, simpleButton = ObjectRepository.SimpleButton.btnPopupSpellOk };
            var actionHandler1 = new ActionHandler(searchCriteria1);
            foo = actionHandler1.SimpleButton_Click(searchCriteria1);
            
            if (!foo)
            {
                var searchCriteria = new SearchCriteria { window = ObjectRepository.Window.Edit1SpellchkWindow, simpleButton = ObjectRepository.SimpleButton.btnSpellCancel };
                var actionHandler = new ActionHandler(searchCriteria);
                foo = actionHandler.SimpleButton_Click(searchCriteria);               
            }

            return foo;
        }

        public bool ConfirmationWindow(string sWindow,string yesno)
        {
            try
            {
                WinControlExtension.ClickOnWinButton(sWindow, yesno);
                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }


        public  bool UA_LOGINTO_ENTRADA(string strUser,string strPass)
        {
            try
            {
                var searchCriteria = new SearchCriteria { window = ObjectRepository.Window.Edit1loginWindow, simpleEdit = ObjectRepository.SimpleTextEdit.loginUser };
                var actionHandler = new ActionHandler(searchCriteria);
                actionHandler.SetText(searchCriteria, strUser);
                searchCriteria.simpleEdit = ObjectRepository.SimpleTextEdit.loginPassword;
                actionHandler.SetText(searchCriteria, strPass);
                searchCriteria.simpleButton = ObjectRepository.SimpleButton.loginButton;
                actionHandler.SimpleButton_Click(searchCriteria);

                Thread.Sleep(90000); 
                 
                 return true;
            }

                


            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw ;
                //return false;
            }
        }

        public bool UA_ENTRADAHOME_CLICKFINDALLJOBS()
        {

            try
            {

                //WinControlExtension.ApplyShortCutyKeys(ObjectRepository.EntradaHomePage.Window,"FindJob");
                WinControlExtension.ClickOnWinButton(ObjectRepository.EntradaHomePage.Window, ObjectRepository.EntradaHomePage.Findalljobs);

                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }

        public void VerifyHomepage(string Value)
        { 
          
            
        
        }

        public bool UA_ENTRADAHOME_SELECTJOB_BYROWID(string rowno)
        {

            try
            {
                WinControlExtension.WinTable_SelecRow(ObjectRepository.EntradaHomePage.Window, ObjectRepository.EntradaHomePage.Row,rowno);

                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }

        public bool UA_ENTRADAHOME_SELECTJOB_BYJOBNAME(string job)
        {

            try
            {
                WinControlExtension.WinTable_SelecCell(ObjectRepository.EntradaHomePage.Window, job);

                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }



        public bool DownloadJobByJobName(string job) //Job name is passed as parameter from TESTCASE SHEET
        {
            try
            {
                var searchCriteria = new SearchCriteria { window = ObjectRepository.Window.Edit1HomePag };
                var actionHandler = new ActionHandler(searchCriteria);
  
                actionHandler.FindJob(job);
                actionHandler.SelectJob();
                               
                WinControlExtension.ClickOnWinButton(ObjectRepository.EntradaHomePage.Window, ObjectRepository.EntradaHomePage.Downloadjob);
                Thread.Sleep(60000);
                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }

        public bool DownloadDefaultJob()//By default first job is selected and downloaded
        {
            try
            {
                var searchCriteria = new SearchCriteria { window = ObjectRepository.Window.Edit1HomePag };
                var actionHandler = new ActionHandler(searchCriteria);
                string sJobId;

                actionHandler.SelectJob();
                sJobId = actionHandler.GetJobId();
                SaveJob(sJobId);

                WinControlExtension.ClickOnWinButton(ObjectRepository.EntradaHomePage.Window, ObjectRepository.EntradaHomePage.Downloadjob);
                Thread.Sleep(60000);
                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }

        public bool DownloadJobSentFromPreviousState()//Job which is sent from previous state
        {
            try
            {                
                string sJob="";
                sJob = GetJob();
                if(sJob!="")
                {
                    return DownloadJobByJobName(sJob);
                }
                else { return false; }
               
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }

        public void SaveJob(string sJob)
        {
            string sfilePath = @"F:\Github\UIAutomation\TestResults\JobInUse.txt";
            FileStream fs = null;
            if (!File.Exists(sfilePath))
            {
                using (fs = File.Create(sfilePath))
                {

                }
                using (StreamWriter sw = new StreamWriter(sfilePath))
                {
                    sw.WriteLine(sJob);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(sfilePath, false))
                {
                    sw.WriteLine(sJob);
                }
            }

        }

        public string  GetJob()
        {
            string sfilePath = @"F:\Github\UIAutomation\TestResults\JobInUse.txt";
            string sJob="";
            if (File.Exists(sfilePath))
            {
                StreamReader sr = new StreamReader(sfilePath);
                String line;

                while ((line = sr.ReadLine()) != null)
                {                   
                    if (line.Trim() != string.Empty)
                    {
                        sJob = line.Trim();
                    }
                }
            }
            else
            {
                sJob = "";
            }
            return sJob;
        }


        public bool UA_ENTRADAHOME_SELECT_MENUITEM(string menuItem)
        {

            try
            {
                
                WinControlExtension.WinTabPage_ClickItem(ObjectRepository.EntradaHomePage.Window, menuItem);

                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }

        public bool UA_RELEASEJOB_CONFIRMATION(string yesno)
        {

            try
            {


                WinControlExtension.ClickOnWinButton(ObjectRepository.Popup.confirmWindow, yesno);

                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
        }

        #endregion
    }
}
