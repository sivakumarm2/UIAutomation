using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.Automation.ObjectUtility;
using Microsoft.VisualStudio.TestTools.UITesting;



namespace UI.Automation.Win
{
    public class UserActions
    {
        public  UserActions()
        {
            Util.AUTPath = @"C:\Users\sivak_000\Desktop\Entrada Editor";
                       
        }

        #region login

        public  bool UA_LOGINTO_ENTRADA(string strUserandPass)
        {
            try
            {
                
                 var window = WinControlExtension.WindowExists(ObjectRepository.EntradaHomePage.Window);
                 if (window == null)
                 {
                     string[] splitarray = strUserandPass.Split('@');
                     WinControlExtension.WinTextBox_SetText_UserId(ObjectRepository.Login.window, ObjectRepository.Login.txtPassword, splitarray[0]);
                     WinControlExtension.WinTextBox_SetText(ObjectRepository.Login.window, ObjectRepository.Login.txtPassword, splitarray[1]);
                     WinControlExtension.ClickOnWinButton(ObjectRepository.Login.window, ObjectRepository.Login.btnlogin);

                     
                 }
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


                WinControlExtension.ApplyShortCutyKeys(ObjectRepository.EntradaHomePage.Window,"FindJob");
                //WinControlExtension.ClickOnWinButton(ObjectRepository.EntradaHomePage.Window, ObjectRepository.EntradaHomePage.Findalljobs);

                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
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



        public bool UA_ENTRADAHOME_DOWNLOADJOB()
        {

            try
            {
                
                WinControlExtension.ClickOnWinButton(ObjectRepository.EntradaHomePage.Window, ObjectRepository.EntradaHomePage.Downloadjob);

                return true;
            }

            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw;
                //return false;
            }
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
