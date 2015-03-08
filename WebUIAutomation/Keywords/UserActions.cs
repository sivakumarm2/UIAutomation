using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.Automation.ObjectUtility;
using Microsoft.VisualStudio.TestTools.UITesting;



namespace UI.Automation.Web.Keywords
{
    public class UserActions
    {
        public  UserActions()
        {
            Util.URL = "http://mail.yahoo.com";
           
        }

        #region login

        public  bool UA_LOGININTO_YAHOO_MAIL(string strUserandPass)
        {
            try
            {
                string[] splitarray = strUserandPass.Split('@');
                Util.Browser.TextBox_SetText(ObjectRepository.Login.txtUserId, splitarray[0]);
                Util.Browser.TextBox_SetText(ObjectRepository.Login.txtPassword, splitarray[1]);
                Util.Browser.ClickOnButton(ObjectRepository.Login.btnSignIn);

                return true;
            }
            catch (Exception e)
            {
                //string str = e.Message.ToString();
                throw ;
                //return false;
            }
        }

        #endregion
    }
}
