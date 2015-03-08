using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;

namespace UI.Automation.ObjectUtility
{
    public class Util
    {
        private static BrowserWindow browser = null;
        private static ApplicationUnderTest _aut=null;

        private static string _url = string.Empty;
        private static string _autpath = string.Empty;

           public static BrowserWindow Browser
        {
            get
            {
                if (string.IsNullOrEmpty(URL)) throw new Exception("Opening URL is not passed.");
                if (browser == null) return browser = BrowserWindow.Launch(URL);
                return browser;
            }

        }

           public static ApplicationUnderTest AUT
           {
               get
               {
                   if (string.IsNullOrEmpty(AUTPath)) throw new Exception("Application under test path is not passed!!");
                   //if (_aut == null) return _aut = ApplicationUnderTest.Launch(_autpath);
                   return _aut;                   
               }
           }

           public static string AUTPath
           {
               get { return _autpath; }
               set
               {
                   if (_autpath != null)
                   {
                       //_aut.Close();
                       _aut = null;
                   }

                   _autpath = value;
               
               }
           
           }



        public static void CloseBrowser()
        {
            if (browser != null)
            {
                browser.Close();
                browser = null;

            }
        }

        public static string URL
        {
            get { return _url; }
            set 
            {
                if (browser != null)
                {
                    browser.Close();
                    browser = null;
                }
                _url = value; 
            }
        }

    }
}
