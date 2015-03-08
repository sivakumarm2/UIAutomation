using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.Automation.ObjectUtility;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UI.Automation.Win
{
    class CheckPoints
    {

        public bool CP_ENTRADAHOME_VALIDATE_BUTTONS_ENABLED(string listofbuttons)
        {
            string[] buttons= listofbuttons.Split('@');
            for(int i=0;i<buttons.Length;i++)
            {
                if (!WinControlExtension.IsButtonEnabled(ObjectRepository.EntradaHomePage.Window, buttons[i]))
                {
                    return false;
                }               
                          
                
            }

            return true;
        }

        public bool CP_ENTRADALOGIN_VALIDATE_INVALIDLOGIN_ERRORTEXT(string errorText)
        {

            string lbltext = string.Empty;
            lbltext=WinControlExtension.WinLabel_Getvalue(ObjectRepository.Login.window, ObjectRepository.Login.lblerrortext);

            if (string.Compare(errorText, lbltext) == 0)
            {
                return true;
            }
            else { return false; }
           
        }

        public bool CP_ENTRADEDITOR_VALIDATE_HOMEPAGE_DISPLAYED()
        {

            var window = WinControlExtension.WindowExists(ObjectRepository.EntradaHomePage.Window);
             if (!(window==null))
             {
                return true;
            }
            else { return false; }

        }

        public bool CP_ENTRADAHOME_VALIDATE_BUTTONS_DISABLED(string listofbuttons)
        {
            string[] buttons = listofbuttons.Split('@');
            for (int i = 0; i < buttons.Length; i++)
            {
                if (WinControlExtension.IsButtonEnabled(ObjectRepository.EntradaHomePage.Window, buttons[i]))
                {
                    return false;
                    
                }

            }

            return true;
        }

        public bool CP_RELEASEJOB_CONFIRMATION_WINDOW_APPEARED()
        {
            var window = WinControlExtension.WindowExists(ObjectRepository.Popup.confirmWindow);
            if (!(window == null))
            {
                return true;
            }
            else { return false; }
            
        }
    }
}
