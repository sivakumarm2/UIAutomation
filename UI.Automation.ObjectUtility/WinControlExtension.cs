namespace UI.Automation.ObjectUtility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;    
    using System.Drawing;
    using System.Reflection;


    public static class WinControlExtension
    {
        
        #region Window
        public static WinWindow WindowExists(string windowName)
        {
            WinWindow window = new WinWindow();
            Playback.PlaybackSettings.SmartMatchOptions = SmartMatchOptions.None;

            window.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);            
            window.SearchProperties.Add(WinWindow.PropertyNames.Name,windowName);            
            if (window.Exists)
            {
                return window;
            }
            else { return null; }
        
        }
        #endregion



        #region WinButoon
        public static void ClickOnWinButton(string windowname, string searchproperties)
        {

            var window = WindowExists(windowname);
            if (!(window == null))
            {             
                WinButton button = new WinButton(window);
                button.SearchProperties.Add(WinButton.PropertyNames.Name, searchproperties);

                if (button.Exists)
                {
                    Mouse.Click(button);
                }
            }
        }

        public static string Winbutton_getvalue(string windowname, string searchproperties)
        {

            string bValue = string.Empty;
            var window = WindowExists(windowname);
            if (!(window == null))
            {
                WinButton button = new WinButton(window);
                button.SearchProperties.Add(WinButton.PropertyNames.Name, searchproperties);

                if (button.Exists)
                {
                    bValue = button.DisplayText;                    
                }
                return bValue;

            }
            else { return bValue; }
        }

            public static bool IsButtonEnabled(string windowname, string searchproperties)
            {
                bool isenabled = true;
                var window = WindowExists(windowname);
                if (!(window == null))
                {
                    WinButton button = new WinButton(window);
                    button.SearchProperties.Add(WinButton.PropertyNames.Name, searchproperties);

                    if (button.Exists)
                    {

                        if (button.Enabled)
                        {
                             isenabled=true;

                        }
                        else
                        {
                            isenabled= false;
                        }
                    }

                }
                return isenabled;
                
            }

        


        #endregion

        #region set text for Textbox

        public static void WinTextBox_SetText(string windowname, string searchproperties, string value)
        {
            var window = WindowExists(windowname);
            if (!(window == null))
            {

                WinEdit textbox = new WinEdit(window);
                textbox.SearchProperties.Add(WinEdit.PropertyNames.Name, searchproperties);

                if (textbox.Exists)
                {
                    textbox.SetFocus();
                    Keyboard.SendKeys(value);
                }
            }
        }

        public static void WinTextBox_SetText_UserId(string windowname, string searchproperties, string value)
        {
            var window = WindowExists(windowname);
            if (!(window == null))
            {
                WinEdit textbox = new WinEdit(window);
                textbox.SearchConfigurations.Add(SearchConfiguration.DisambiguateChild);

                if (textbox.Exists)
                {
                    textbox.Text = value;
                }
            }
        }

        #endregion

        #region Get Label Tet
        public static string WinLabel_Getvalue(string windowname, string searchproperties)
        {
            string labeltext = string.Empty;
            var window = WindowExists(windowname);
            if (!(window==null))
            {  
                WinText text = new WinText(window);
                text.SearchProperties.Add(WinText.PropertyNames.Name, "User name or password not", PropertyExpressionOperator.Contains);

                if (text.Exists)
                {
                    labeltext = text.DisplayText;
                }
            
            }
            return labeltext;
        }

#endregion


        public static void DevvRibbonTextBox_Getvalue(string windowname, string searchproperties, string value)
        {
            var window = WindowExists(windowname);
            if (!(window == null))
            {                
                WinEdit textbox = new WinEdit(window);
                textbox.SearchConfigurations.Add(SearchConfiguration.DisambiguateChild);

                if (textbox.Exists)
                {
                    textbox.Text = value;
                }
            }
        }

        #region Table
        public static void WinTable_SelecRow(string windowname, string searchproperties, string value)
        {

            var window = WindowExists(windowname);
            if (!(window == null))
            {
                WinRow TableRow = new WinRow(window);
                TableRow.SearchProperties.Add(WinEdit.PropertyNames.Name, searchproperties + " "+value);

                if (TableRow.Exists)
                {

                    Mouse.Click(TableRow);
                }
            }


        }

        public static void WinTable_SelecCell(string windowname, string searchproperties)
        {
            var window = WindowExists(windowname);
            if (!(window == null))
            {
                
                WinCell TabbleCell = new WinCell(window);
                TabbleCell.SearchProperties.Add(WinCell.PropertyNames.Value, searchproperties);

                if (TabbleCell.Exists)
                {

                    Mouse.Click(TabbleCell);
                }
            }
        }


        #endregion
        #region Tabpage operations
        public static void WinTabPage_ClickItem(string windowName, string searchProperties)
        {
            var window = WindowExists(windowName);
            if (!(window == null))
            {

                WinTabPage tItem = new WinTabPage(window);
                tItem.SearchProperties.Add(WinEdit.PropertyNames.Name, searchProperties);

                if (tItem.Exists)
                {

                    Mouse.Click(tItem);
                }
            }
        }
        #endregion

        #region ShortKeys
        public static void ApplyShortCutyKeys(string windowname, string value)
        { 
            var window = WindowExists(windowname);
            if (!(window == null))
            {
                window.SetFocus();
                switch (value)
                {
                    case "FindJob":
                        Keyboard.SendKeys("%{Q}");
                        break;
                }
            }
        
        }
        #endregion

    }
}

