namespace UI.Automation.ObjectUtility
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using DevExpress.CodedUIExtension.DXTestControls.v14_2;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Threading;

    
    public interface iControl
    {
         SearchCriteria Criteria { get; set; }
    }
    public class ActionHandler
    {
        private DXControlExtension mControlExtension;
        public ActionHandler(SearchCriteria searchCriteria)
        {
            mControlExtension = new DXControlExtension(searchCriteria);
        }

        public  bool SetText(SearchCriteria searchCriteria, string sValue)
        {
           var txt= mControlExtension.EntradaWindow.getSimpleText(searchCriteria.simpleEdit);
           return txt.SetText(sValue);
        }

        public bool SimpleButton_Click(SearchCriteria searchCriteria)
        {
            var btn = mControlExtension.EntradaWindow.getSimpleButton(searchCriteria.simpleButton);
            return btn.SimpleButton_Click();
        }

        public bool PopupButton_Click(SearchCriteria searchCriteria)
        {
            var btn = mControlExtension.ItemWindow.ItemPopupWindow.getSimpleButton(searchCriteria.simpleButton);
            return btn.SimpleButton_Click();
        }

        public bool RibbonPage_Click(SearchCriteria searchCriteria)
        {
            var RibPage = mControlExtension.EntradaWindow.Ribboncontrol.getRibbonPage(searchCriteria.ribPage);
            return RibPage.RibbonPage_Click();
        }

        public bool ButtonItem_Click(SearchCriteria searchCriteria)
        {
            var RibBtnItem = mControlExtension.EntradaWindow.Ribboncontrol.getRibbonPage(searchCriteria.ribPage).getPageGroup(searchCriteria.ribPagegroup).getRibbonButtonItem(searchCriteria.ribButtonItem);
            return RibBtnItem.ButtonItem_Click();
        }

        //For Select Category 
        public bool RibEditItem_SetValue(SearchCriteria searchCriteria,string sValue)
        {
            var RibEditItm = mControlExtension.EntradaWindow.Ribboncontrol.getRibbonPage(searchCriteria.ribPage).getPageGroup(searchCriteria.ribPagegroup).getRibbonEditItem(searchCriteria.ribEditItem);
            return RibEditItm.EditItem_SetValule(sValue);

        }

        public bool StatusbarItem_Click(SearchCriteria searchCriteria)
        {
            var StatusbarItem = mControlExtension.EntradaWindow.getStatusbar(searchCriteria.ribStatusMenubar).getStatusbarMenuItem(searchCriteria.StatusMenubarItem);
            return StatusbarItem.Click();

        }    

        public bool TextEdit_SetText(SearchCriteria searchCriteria,string value)
        {

            if (searchCriteria.dockPanel != null)
            {
                var edit = mControlExtension.EntradaWindow.getPanel(searchCriteria.dockPanel).getPanelContainer(searchCriteria.docPanelContainer).getTextEdit(searchCriteria.memoEdit);
                return edit.SetText(value);
            }
            else
            {
                var edit = mControlExtension.EntradaWindow.getTextEdit(searchCriteria.memoEdit);
                return edit.SetText(value);
            }
            
        }

        public bool SubMenuItem_SelectItem(SearchCriteria searchCriteria)
        {

            var menuItem = mControlExtension.ItemWindow.SubMenuBar.getSubmenuItemList(searchCriteria.SubMenubarItemIndex);
            return menuItem.SelectItem();
        }
        public void ApplyHotKey(string key)
        {
            var win = mControlExtension.EntradaWindow;
            win.ApplyHotKey(key);

        }

        public bool SelectJob()
        {
            #region Variable Declarations
            var dCell = mControlExtension.EntradaWindow.DocumentsHostTabList.DocContainer.TabClient.GridControlTable.gridCell;
            return dCell.ClickCell();
            #endregion

        }

        public string GetJobId()
        {
            #region Variable Declarations
            var dCell = mControlExtension.EntradaWindow.DocumentsHostTabList.DocContainer.TabClient.GridControlTable.gridCell;
            return dCell.getCellValue();
            #endregion

        }

        public bool FindJob(string sJob)
        {
            #region Variable Declarations
            var gridEdit = mControlExtension.EntradaWindow.DocumentsHostTabList.DocContainer.TabClient.GridControlTable.gridColEdit;
            bool foo = false;
            if (gridEdit.WaitForControlExist(180000))
            {
                foo= gridEdit.SetCellValue(sJob);
                
            }
            return foo;
          
        }
       

    }


    public class ActionHandlerForClassicKeyword   
    {       
        public bool SetText(string path, string sValue)
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            var txt = mControlExtension.EntradaWindow.getSimpleText(searchCriteria.simpleEdit);
            return txt.SetText(sValue);
        }

        public bool SimpleButton_Click(string path)
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            var btn = mControlExtension.EntradaWindow.getSimpleButton(searchCriteria.simpleButton);
            return btn.SimpleButton_Click();
        }

        public bool PopupButton_Click(string path)       
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            var btn = mControlExtension.ItemWindow.ItemPopupWindow.getSimpleButton(searchCriteria.simpleButton);
           
            
            return btn.SimpleButton_Click();
        }

        public bool ButtonItem_Click(string path)
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            var RibBtnItem = mControlExtension.EntradaWindow.Ribboncontrol.getRibbonPage(searchCriteria.ribPage).getPageGroup(searchCriteria.ribPagegroup).getRibbonButtonItem(searchCriteria.ribButtonItem);
            return RibBtnItem.ButtonItem_Click();
        }

        //For Select Category 
        public bool RibEditItem_SetValue(string path, string sValue)
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            var RibEditItm = mControlExtension.EntradaWindow.Ribboncontrol.getRibbonPage(searchCriteria.ribPage).getPageGroup(searchCriteria.ribPagegroup).getRibbonEditItem(searchCriteria.ribEditItem);
            return RibEditItm.EditItem_SetValule(sValue);

        }


        public bool StatusbarItem_Click(string path)
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            var StatusbarItem = mControlExtension.EntradaWindow.getStatusbar(searchCriteria.ribStatusMenubar).getStatusbarMenuItem(searchCriteria.StatusMenubarItem);
            return StatusbarItem.Click();

        }

        public bool TextEdit_SetText(string path, string value)
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            
            if (searchCriteria.dockPanel != null)
            {
                var edit = mControlExtension.EntradaWindow.getPanel(searchCriteria.dockPanel).getPanelContainer(searchCriteria.docPanelContainer).getTextEdit(searchCriteria.memoEdit);
                return edit.SetText(value);
            }
            else
            {
                var edit = mControlExtension.EntradaWindow.getTextEdit(searchCriteria.memoEdit);
                return edit.SetText(value);
            }
        }

        public bool SubMenuItem_SelectItem(string path)
        {
            var searchCriteria = new SearchCriteria(path);
            var mControlExtension = new DXControlExtension(searchCriteria);
            var menuItem = mControlExtension.ItemWindow.SubMenuBar.getSubmenuItemList(searchCriteria.SubMenubarItemIndex);
            return menuItem.SelectItem();
        }

    }

    
    public partial class DXControlExtension
    {
        // Fields
     
        private SearchCriteria msearchCriteria;

        public DXControlExtension(SearchCriteria searchCriteria)
        {
            msearchCriteria = searchCriteria;
            EntradaWindow = new EntradaWindow(msearchCriteria);
            ItemWindow = new WinItemWindow(msearchCriteria);
        
        }

        public EntradaWindow EntradaWindow
        {
            get;
            set;
        }

        public WinItemWindow ItemWindow
        {
            get;
            set;
        }

       
       
    }
    

    public  class SearchCriteria
    {
        
        public SearchCriteria()
        {
        
        }

        public SearchCriteria(string path)
        {             
            var cControlList = path.Split('@');
            foreach (var item in cControlList)
            {
                var ctlList = item.Split(':');
                if (ctlList.Length == 2)
                {
                    
                    switch (ctlList[0].ToUpper()) // Object Type
                    {
                        case "WIN":
                            this.window = ctlList[1];
                            break;
                        case "SUBWIN":
                            this.subWindow = ctlList[1];
                            break;
                        case "RIBPAGE":
                            this.ribPage = ctlList[1];
                            break;                            
                        case "RIBCONTROL":
                            this.ribControl = ctlList[1];
                            break;
                        case "RIBPAGEGROUP":
                            this.ribPagegroup = ctlList[1];
                            break;
                        case "RIBBARBUTTONGROUP":
                            this.ribbarButtonGroup = ctlList[1];
                            break;
                        case "RIBEDITITEM":
                            this.ribEditItem = ctlList[1];
                            break;
                        case "RIBBUTTONITEM":
                            this.ribButtonItem = ctlList[1];
                            break;
                        case "SIMPLEEDIT":
                            this.simpleEdit = ctlList[1];
                            break;
                        case "SIMPLEBUTTON":
                            this.simpleButton = ctlList[1];
                            break;
                        case "SIMPLELABEL":
                            this.simpleLabel = ctlList[1];
                            break;
                        case "DOCPANEL":
                            this.dockPanel = ctlList[1];
                            break;
                        case "PANELCONTAINER":
                            this.docPanelContainer = ctlList[1];
                            break;
                        case "MEMOEDIT":
                            this.memoEdit = ctlList[1];
                            break;
                        case "STATUSMENU":
                            this.ribStatusMenubar = ctlList[1];
                            break;
                        case    "BARITEM":
                            this.StatusMenubarItem = ctlList[1];
                            break;
                        case "SUBMENUBAR":
                            this.SubMenubar = ctlList[1];
                            break;
                        case "SUBMENUBARITEM":
                            this.SubMenubarItemIndex = ctlList[1];
                            break;
                        default:
                            break;
                    
                    }
                    
                }
            }
        
        }

        public string window {get;set;}
        public string subWindow { get; set; }
        public string ribPage { get; set; }
        public string ribControl { get; set; }
        public string ribPagegroup { get; set; }
        public string ribbarButtonGroup { get; set; }
        public string ribButtonItem { get; set; }
        public string ribEditItem { get; set; }
        public string simpleEdit { get; set; }
        public string simpleButton { get; set; }
        public string simpleLabel { get; set; }
        public string dockPanel { get; set; }
        public string docPanelContainer { get; set; }
        public string memoEdit { get; set; }
        public string ribStatusMenubar { get; set; }
        public string StatusMenubarItem { get; set; }
        public string SubMenubar { get; set; }
        public string SubMenubarItemIndex { get; set; }
        public string targetObject { get; set; }
        
    }


    public class WinItemWindow : WinWindow
    {
        private SearchCriteria mSearchCriteria;
        string sWindow;

       

        public WinItemWindow(SearchCriteria sarchCritera)
        {
            #region Search Criteria
            mSearchCriteria = sarchCritera;
            sWindow = sarchCritera.window;
            this.SearchProperties[WinWindow.PropertyNames.AccessibleName] = sWindow;// "Desktop";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32769";
            #endregion
        }



        public PopupWindow ItemPopupWindow
        {
            get
            {
                if ((this.mItemPopupWindow == null))
                {
                    this.mItemPopupWindow = new PopupWindow(this,this.mSearchCriteria);
                }
                return this.mItemPopupWindow;
            }
        }

        #region Properties
        public SubMenuBar SubMenuBar
        {
            get
            {
                if ((this.mSubMenuBar == null))
                {
                    this.mSubMenuBar = new SubMenuBar(this,this.mSearchCriteria);
                }
                return this.mSubMenuBar;
            }
        }
        #endregion

        #region Fields
        private SubMenuBar mSubMenuBar;
        private PopupWindow mItemPopupWindow;
        #endregion
    }

    public class PopupWindow : DXWindow,iControl
    {

        private SearchCriteria mSearchCriteria;
        string sWindow;
        Dictionary<string, PopupSimpleButton> mButtonList = new Dictionary<string, PopupSimpleButton>();

        public PopupWindow(UITestControl searchLimitContainer,SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            mSearchCriteria = searchCriteria;
            sWindow = searchCriteria.subWindow;
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = sWindow;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "XtraMessageBoxForm";
            #endregion
        }

        public PopupSimpleButton getSimpleButton(string name)
        {

            if (!mButtonList.ContainsKey(name))
            {
                var btn = new PopupSimpleButton(this, mSearchCriteria);
                mButtonList.Add(name, btn);
            }
            return this.mButtonList[name];

        }




        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }


    public class SubMenuBar : DXMenu,iControl
    {

        Dictionary<string, SubMenubarItem> SubMenubarItemList = new Dictionary<string, SubMenubarItem>();
        public SubMenuBar(UITestControl searchLimitContainer,SearchCriteria searchcriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchcriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.SubMenubar;//"SubMenuBarControl";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "SubMenuBarControl";
            #endregion
        }

        #region Properties
        public SubMenubarItem getSubmenuItemList(string name)
        {

            if (!SubMenubarItemList.ContainsKey(name))
            {
                var ItemList = new SubMenubarItem(this, Criteria);
                SubMenubarItemList.Add(name, ItemList);
            }
            return this.SubMenubarItemList[name];

        }
        #endregion

       

        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }

    public class SubMenubarItem:DXMenuBaseButtonItem,iControl
    {

        public SubMenubarItem(UITestControl searchLimitContainer, SearchCriteria searchcriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchcriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] ="BarButtonItemLink["+ Criteria.SubMenubarItemIndex +"]";//"BarButtonItemLink[2]";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "MenuBaseButtonItem";
            #endregion
        }

        public bool SelectItem()
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                Mouse.Click(this);
                foo = true;
            }
            return foo;
        }

        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }



    public class EntradaWindow : DXWindow
    {
        //Fields
        private RibbonControl mRibbonControl;
        private SimpleTextEdit mSimpleText;
        private SimpleLable mSimpleLabel;
        private SimpleButton mSimpleButton;
        private SearchCriteria mSearchCriteria;
        private DockPanel mDocpanel;

        string sWindow;

        Dictionary<string, SimpleTextEdit> mTextEditList = new Dictionary<string, SimpleTextEdit>();
        Dictionary<string, SimpleButton> mButtonList = new Dictionary<string, SimpleButton>();
        Dictionary<string, SimpleLable> mLabelList = new Dictionary<string, SimpleLable>();
        Dictionary<string, DockPanel> PanlList = new Dictionary<string, DockPanel>();
        Dictionary<string, RibbonStatusBar> StatusbarList = new Dictionary<string, RibbonStatusBar>();
        Dictionary<string, TextEdit> TextEditList = new Dictionary<string, TextEdit>();

        public EntradaWindow(SearchCriteria sarchCritera)
        {
            mSearchCriteria = sarchCritera;

            #region Search Criteria;
            sWindow=sarchCritera.window;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = sWindow;
            this.SearchProperties.Add(new PropertyExpression(DXTestControl.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add(sWindow);
            #endregion
        }

       
        public RibbonControl Ribboncontrol
        {
            get
            {
                if ((this.mRibbonControl == null))
                {
                    this.mRibbonControl = new RibbonControl(this,this.mSearchCriteria);
                }
                return this.mRibbonControl;
            }
        }

        #region Properties
        public DocumentsHostTabList DocumentsHostTabList
        {
            get
            {
                if ((this.mDocumentsHostTabList == null))
                {
                    this.mDocumentsHostTabList = new DocumentsHostTabList(this);
                }
                return this.mDocumentsHostTabList;
            }
        }
        #endregion

        #region Fields
        private DocumentsHostTabList mDocumentsHostTabList;
        #endregion

       
        public void ApplyHotKey(string hotykey)
        {
            this.SetFocus();
            Thread.Sleep(10000);
            Keyboard.SendKeys("{" + hotykey +"}");
            Thread.Sleep(10000);

        }
        
        public DockPanel getPanel(string name)
        {

            if (!PanlList.ContainsKey(name))
            {
                var panel = new DockPanel(this, this.mSearchCriteria);
                PanlList.Add(name, panel);
            }
            return this.PanlList[name];

        }

        public TextEdit getTextEdit(string name)
        {

            if (!TextEditList.ContainsKey(name))
            {
                var edit = new TextEdit(this, this.mSearchCriteria);
                TextEditList.Add(name, edit);
            }
            return this.TextEditList[name];

        }     

        public RibbonStatusBar getStatusbar(string name)
        {

            if (!StatusbarList.ContainsKey(name))
            {
                var statusbar = new RibbonStatusBar(this, this.mSearchCriteria);
                StatusbarList.Add(name, statusbar);
            }
            return this.StatusbarList[name];

        }


        public SimpleTextEdit getSimpleText(string name)
        {

                if (!mTextEditList.ContainsKey(name))
                {                    
                   var txt = new SimpleTextEdit(this,mSearchCriteria);
                   mTextEditList.Add(name, txt);
                }
                return this.mTextEditList[name];
            
        }

        public SimpleLable getSimpleLable(string name)
        {
            if (!mLabelList.ContainsKey(name))
            {
                var lbl = new SimpleLable(this, mSearchCriteria);
                mLabelList.Add(name, lbl);
            }
            return this.mLabelList[name];
            
        }

        public SimpleButton getSimpleButton(string name)
        {

            if (!mButtonList.ContainsKey(name))
            {
                var btn = new SimpleButton(this, mSearchCriteria);
                mButtonList.Add(name, btn);
            }
            return this.mButtonList[name];
            
        }        
        
    }

    public class DocumentsHostTabList : DXTestControl
    {

        public DocumentsHostTabList(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "DocumentsHost[0]";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "DocumentsHost";
            this.WindowTitles.Add("Entrada Editor");
            #endregion
        }

        #region Properties
        public DocumentContainer DocContainer
        {
            get
            {
                if ((this.mDocContainer == null))
                {
                    this.mDocContainer = new DocumentContainer(this);
                }
                return this.mDocContainer;
            }
        }
        #endregion

        #region Fields
        private DocumentContainer mDocContainer;
        #endregion
    }


    public class DocumentContainer : DXTestControl
    {

        public DocumentContainer(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "Available Jobs";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "DocumentContainer";
            this.WindowTitles.Add("Entrada Editor");
            #endregion
        }

        #region Properties
        public TabClient TabClient
        {
            get
            {
                if ((this.mUIAvailableJobsTabClient == null))
                {
                    this.mUIAvailableJobsTabClient = new TabClient(this);
                }
                return this.mUIAvailableJobsTabClient;
            }
        }
        #endregion

        #region Fields
        private TabClient mUIAvailableJobsTabClient;
        #endregion
    }

    
    public class TabClient : DXTestControl
    {

        public TabClient(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "Available JobsAvailableJobsTab[0]";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "AvailableJobsTab";
            this.WindowTitles.Add("Entrada Editor");
            #endregion
        }

        #region Properties
        public GridControlTable GridControlTable
        {
            get
            {
                if ((this.mGridControlTable == null))
                {
                    this.mGridControlTable = new GridControlTable(this);
                }
                return this.mGridControlTable;
            }
        }
        #endregion

        #region Fields
        private GridControlTable mGridControlTable;
        #endregion
    }

   
    public class GridControlTable : DXGrid
    {

        public GridControlTable(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "Available JobsAvailableJobsTab[0]GridControl[0]";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GridControl";
            this.WindowTitles.Add("Entrada Editor");
            #endregion
        }

        #region Properties
        public GridCell gridCell
        {
            get
            {
                if ((this.mgridCell == null))
                {
                    this.mgridCell = new GridCell(this);
                }
                return this.mgridCell;
            }
        }

        public GridColumnEdit gridColEdit
        {
            get
            {
                if ((this.mgridColEdit == null))
                {
                    this.mgridColEdit = new GridColumnEdit(this);
                }
                return this.mgridColEdit;
            }
        }
        #endregion

        #region Fields
        private GridCell mgridCell;

        private GridColumnEdit mgridColEdit;
        #endregion
    }


    public class GridCell : DXCell
    {

        public GridCell(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "Available JobsAvailableJobsTab[0]GridControl[0]GridControlCell[View][Row]0[Column" +
                     "]colJobNumber";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GridControlCell";
            this.WindowTitles.Add("Entrada Editor");


        }

        public bool ClickCell()
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                Mouse.Click(this);
                foo = true;
            }
            return foo;
        }

        public string getCellValue()
        {
            string sCellVaue=null;

            if (this.WaitForControlExist(20000))
            {
                sCellVaue = this.GetProperty("Text").ToString();
                
            }
            return sCellVaue;
        
        }
    }

    public class GridColumnEdit : DXTextEdit
    {

        public GridColumnEdit(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "Available JobsAvailableJobsTab[0]GridControl[0]TextEdit[View][Row]-999997[Column]" +
                "colJobNumber";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "TextEdit";
            this.WindowTitles.Add("Entrada Editor");
            #endregion
        }

        public bool SetCellValue(string value)
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                foo = true;
                this.SetProperty("Text", value);
            }
            return foo;   

        }

    }

    public class SimpleTextEdit: DXTextEdit
    {

        public SimpleTextEdit( UITestControl searchLimitContainer, SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            this.SearchProperties[DXTestControl.PropertyNames.Name] = searchCriteria.simpleEdit;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "TextEdit";
            this.WindowTitles.Add(searchCriteria.window);       
        
        }

        public bool SetText(string sValue)
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                foo = true;
                this.SetProperty("Text", sValue);
            }
            return foo;            
        }

        public bool verifyText(string sExpectedVal)
        {
             bool foo = false;
             if (this.WaitForControlExist(20000))
             {                 
                 string sActualval = this.GetProperty("Text").ToString();
                 if(string.Compare(sActualval,sExpectedVal)==0)
                 {
                     foo = true;
                 }
             }
             return foo;
        }

            
    }


    public class SimpleButton:DXTestControl  
    {
        public SimpleButton(UITestControl searchLimitContainer, SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            this.SearchProperties[DXTestControl.PropertyNames.Name] = searchCriteria.simpleButton;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "SimpleButton";
            this.WindowTitles.Add(searchCriteria.window);
        
        
        }

        public bool SimpleButton_Click()      
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                Mouse.Click(this);
                foo = true;
            }
            return foo;
        }


        public bool SimpleButton_Verifytext(string sExpectedVal)
        {
            bool foo = false;

            if (this.WaitForControlExist(20000))
            {
                string sItemValActual = this.GetProperty("Text").ToString();
                if (string.Compare(sExpectedVal, sItemValActual) == 0)
                {
                    foo = true;
                }
            }
            return foo;
        }
    
    }

    public class PopupSimpleButton : DXTestControl
    {
        public PopupSimpleButton(UITestControl searchLimitContainer, SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            this.SearchProperties[DXTestControl.PropertyNames.Name] = searchCriteria.simpleButton;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "SimpleButton";
            this.WindowTitles.Add(searchCriteria.subWindow);


        }

        public bool SimpleButton_Click()
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                Mouse.Click(this);
                foo = true;
            }
            return foo;
        }
    }
    public class SimpleLable:DXTestControl
    {

        public SimpleLable(UITestControl searchLimitContainer, SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            this.SearchProperties[DXTestControl.PropertyNames.Name] = searchCriteria.simpleLabel;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "LabelControl";
            this.WindowTitles.Add(searchCriteria.window);        
        
        }

        public bool SimpleLabel_Verifytext(string sExpectedVal)
        {
            bool foo = false;
            
            if (this.Exists)
            {
                string sItemValActual = this.GetProperty("Text").ToString();
                if (string.Compare(sExpectedVal, sItemValActual) == 0)
                {
                    foo = true;
                }
            }
            return foo;
        }

    }

    public class RibbonStatusBar : DXMenu,iControl
    {

        Dictionary<string, StatusbarMenuItem> barItemList = new Dictionary<string, StatusbarMenuItem>();
        public RibbonStatusBar(UITestControl searchLimitContainer,SearchCriteria searchcriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchcriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.ribStatusMenubar;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonStatusBar";
            this.WindowTitles.Add(Criteria.window);
            #endregion
        }

        #region Properties
        public StatusbarMenuItem getStatusbarMenuItem(string name)
        {

            if (!barItemList.ContainsKey(name))
            {
                var barItem = new StatusbarMenuItem(this, this.Criteria);
                barItemList.Add(name, barItem);
            }
            return this.barItemList[name];

        }
        #endregion

       
        public SearchCriteria Criteria
        {
            get;
            set;

        }
    }

    public class StatusbarMenuItem:DXMenuItem,iControl
    {

        public StatusbarMenuItem(UITestControl searchLimitContainer, SearchCriteria searchcriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchcriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.StatusMenubarItem;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "BarItem";
            this.WindowTitles.Add(Criteria.window);
            #endregion
        }

        public bool Click()
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                Mouse.Click(this);
                foo = true;
            }
            return foo;
        }

        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }

    public class RibbonControl : DXRibbon,iControl
    {
        //Fields
        private RibbonPage mRibbonPage;

        Dictionary<string, RibbonPage> mRibPageList = new Dictionary<string, RibbonPage>();
        
        public RibbonControl(UITestControl searchLimitContainer, SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            Criteria = searchCriteria;
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = searchCriteria.ribControl;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonControl";
            this.WindowTitles.Add(searchCriteria.window);
            #endregion

        }

       
        public RibbonPage getRibbonPage(string name)
        {

            if (!mRibPageList.ContainsKey(name))
            {
                var RibPage = new RibbonPage(this, Criteria);
                mRibPageList.Add(name, RibPage);
            }
            return this.mRibPageList[name];            
           
        }

        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }

    public class RibbonPage : DXRibbonPage,iControl
    {

        private RibbonPageGroup mPageGroup;
        Dictionary<string, RibbonPageGroup> RibPageGroupList = new Dictionary<string, RibbonPageGroup>();

        public RibbonPage(UITestControl searchLimitContainer, SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchCriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = searchCriteria.ribPage;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonPage";
            this.WindowTitles.Add(searchCriteria.window);
            #endregion
        }
              

        public RibbonPageGroup getPageGroup(string name)
        {
            if (!RibPageGroupList.ContainsKey(name))
            {
                var RibPage = new RibbonPageGroup(this, Criteria);
                RibPageGroupList.Add(name, RibPage);
            }
            return this.RibPageGroupList[name];
        }


        public SearchCriteria Criteria
        {
            get;
            set;
        }

        public bool RibbonPage_Click()
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                Mouse.Click(this);
                foo = true;
            }
            return foo;
        }


    }

    public class RibbonPageGroup : DXRibbonPageGroup,iControl
    {
       
        private DXRibbonButtonItem mRibbonButtonItem;
        private RibbonBarbutton mbarRibbonButton;

        Dictionary<string, RibbonbuttonItem> RibButtonItemList = new Dictionary<string, RibbonbuttonItem>();
        Dictionary<string, RibbonBarbutton> RibBarButtonList = new Dictionary<string, RibbonBarbutton>();
        Dictionary<string, RibbonEditItem> RibEditItemList = new Dictionary<string, RibbonEditItem>();

        public RibbonPageGroup(UITestControl searchLimitContainer,SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            Criteria = searchCriteria;
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = searchCriteria.ribPagegroup;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonPageGroup";
            this.WindowTitles.Add(searchCriteria.window);
            #endregion
        }

        public RibbonbuttonItem getRibbonButtonItem(string name)
        {

            if (!RibButtonItemList.ContainsKey(name))
            {
                var RibButtonItem = new RibbonbuttonItem(this, Criteria);
                RibButtonItemList.Add(name, RibButtonItem);
            }
            return this.RibButtonItemList[name];
          
        }

        public RibbonBarbutton getRibbonbarButton(string name)
        {

            if (!RibBarButtonList.ContainsKey(name))
            {
                var RibBarBtn = new RibbonBarbutton(this, Criteria);
                RibBarButtonList.Add(name, RibBarBtn);
            }
            return this.RibBarButtonList[name];
          
        }

        public RibbonEditItem getRibbonEditItem(string name)
        {

            if (!RibEditItemList.ContainsKey(name))
            {
                var RibButtonItem = new RibbonEditItem(this, Criteria);
                RibEditItemList.Add(name, RibButtonItem);
            }
            return this.RibEditItemList[name];

        }
        
        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }

     public class RibbonbuttonItem:DXRibbonButtonItem,iControl
    {
        
        public RibbonbuttonItem(UITestControl searchLimitContainer,SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria=searchCriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = this.Criteria.ribButtonItem;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonBaseButtonItem";
            this.WindowTitles.Add(this.Criteria.window);
            #endregion
        }

        public SearchCriteria Criteria
        {
            get;
            set;
        }

        public bool ButtonItem_Click()
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                Mouse.Click(this);
                foo = true;
            }
            return foo;
        }

         public bool ButtonItem_Verifytext(string sExpectedVal)
         {
            bool foo = false;
            
            if (this.Exists)
            {
                string sItemValActual = this.GetProperty("Text").ToString();
                if (string.Compare(sExpectedVal, sItemValActual) == 0)
                {
                    foo = true;
                }
            }
            return foo;
         }

         public bool ButtonItem_IsEnabled()
         {

             bool foo = false;
             if (this.Exists)
             {
                 foo= Convert.ToBoolean(this.GetProperty("Enabled"));
             }

             return foo;
            
         }

         public bool ButtonItem_IsDisabled()
         {

              bool foo = false;
             if (this.Exists)
             {
                 foo= Convert.ToBoolean(this.GetProperty("Enabled"));
             }

             return foo;
         }


     }

    public class RibbonBarbutton:DXRibbonItem,iControl
    {
        private DXRibbonEditItem mRibbonEditItem;

        Dictionary<string, RibbonEditItem> RibEditItemList = new Dictionary<string, RibbonEditItem>();
        public RibbonBarbutton(UITestControl searchLimitContainer,SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchCriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.ribbarButtonGroup;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonItem";
            this.WindowTitles.Add(Criteria.window);
            #endregion            
            
        }


        public RibbonEditItem getRibbonEditItem(string name)
        {

            if (!RibEditItemList.ContainsKey(name))
            {
                var RibButtonItem = new RibbonEditItem(this, Criteria);
                RibEditItemList.Add(name, RibButtonItem);
            }
            return this.RibEditItemList[name];

        }      


        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }

    public class RibbonEditItem : DXRibbonEditItem, iControl
    {


        public RibbonEditItem (UITestControl searchLimitContainer,SearchCriteria searchCriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchCriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.ribEditItem;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonEditItem";
            this.WindowTitles.Add(Criteria.window);
            #endregion

        }

        public SearchCriteria Criteria
        {
            get;
            set;

        }

        public bool EditItem_SetValule(string sValue)
        {
            bool foo = false; 
            if (this.WaitForControlExist(20000))
             {
                 this.SetProperty("ValueAsString", sValue);
                 foo = true;
             }
            return foo;
        
        }

        public bool EditItem_VerifyValue(string sExpectedVal)
        {

            bool foo = false;

            if (this.Exists)
            {
                string sItemValActual = this.GetProperty("ValueAsString").ToString();
                if (string.Compare(sExpectedVal, sItemValActual) == 0)
                {
                    foo = true;
                }

            }

            return foo;
        }

    }


    public class DockPanel : DXDockPanel,iControl
    {
        private PanelContiner mPanelContainer;

        Dictionary<string, PanelContiner> PanelContainerList = new Dictionary<string, PanelContiner>();
        public DockPanel(UITestControl searchLimitContainer,SearchCriteria searchcriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchcriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.dockPanel;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "DockPanel";
            this.WindowTitles.Add(Criteria.window);
            #endregion
        }

        #region Properties
         public PanelContiner getPanelContainer(string name)
        {

            if (!PanelContainerList.ContainsKey(name))
            {
                var pcontainer = new PanelContiner(this, this.Criteria);
                PanelContainerList.Add(name, pcontainer);
            }
            return this.PanelContainerList[name];

        }     
        
        #endregion

        #region Fields
        private PanelContiner mUIDockPanel5_ContainerCustom;
        #endregion

        public SearchCriteria Criteria
        {
            get;
            set;

        }
    }

 
    public class PanelContiner : DXTestControl,iControl
    {
        #region Fields
        private TextEdit mTextEdit;
        #endregion

        Dictionary<string, TextEdit> TextEditList = new Dictionary<string, TextEdit>();
        public PanelContiner(UITestControl searchLimitContainer,SearchCriteria searchcriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchcriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.docPanelContainer;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "ControlContainer";
            this.WindowTitles.Add(Criteria.window);
            #endregion
        }

        #region Properties

        public TextEdit getTextEdit(string name)
        {

            if (!TextEditList.ContainsKey(name))
            {
                var edit = new TextEdit(this, this.Criteria);
                TextEditList.Add(name, edit);
            }
            return this.TextEditList[name];

        }     
        
        #endregion

        
        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }

    public class TextEdit:DXTextEdit,iControl
    {

        public TextEdit(UITestControl searchLimitContainer, SearchCriteria searchcriteria) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            Criteria = searchcriteria;
            this.SearchProperties[DXTestControl.PropertyNames.Name] = Criteria.memoEdit;
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "MemoEdit";
            this.WindowTitles.Add(Criteria.window);
            #endregion
        }

        public bool SetText(string sValue)
        {
            bool foo = false;
            if (this.WaitForControlExist(20000))
            {
                foo = true;
                this.SetProperty("Text", sValue);
            }
            return foo; 
        }

        public SearchCriteria Criteria
        {
            get;
            set;
        }
    }


}


            #endregion