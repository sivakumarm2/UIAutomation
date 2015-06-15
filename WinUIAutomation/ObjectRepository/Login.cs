using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Automation.Win.ObjectRepository
{
     class Login
    {

         public const string window = "Entrada Editor Login";
         public const string txtUserId = "txtUser";
         public const string txtPassword = "PasswordTextEdit";
         public const string btnlogin = "Log In";
         public const string lblerrortext = "lblError";
    }


     public class Window
     {
         public const string Edit1loginWindow = "Entrada Editor Login";
         public const string Edit1HomePag = "Entrada Editor";
         public const string Edit1SpellchkWindow = "Spelling";
         public const string SpellingPopup = "Desktop";
         public const string SpellingPopupSubWin = "Editor";
     }

     public class RibbonControl
     {
         public const string Edit1HomePageRibbonControl = "ribbonControl1";
         
     }

     public class RibbonPage
     {
         public const string ribbonPageFormat = "homeRibbonPage1";
         public const string ribbonPageHome = "ribbonPage1";
         public const string ribbonPageAdvanced = "ribbonPage2";
       
     }

     public class RibbonPageGroup
     {
        //Home
        public const string ribbongrpHomeDownloadQueue = "grpDownloadQueue";
        public const string ribbongrpHomeDocuments="grpDocuments";
        public const string ribbongrpHomeclientData="grpClientData";
        public const string ribbongrpHomeAudio = "grpAudio ";

         //Format
        public const string ribbongrpFormatFont="fontRibbonPageGroup1";
        public const string ribbongrpFormatParagraph="paragraphRibbonPageGroup1";
        public const string ribbongrpFormatQuickFix = "grpQuickFix";
        public const string ribbongrpFormatEdit = "editingRibbonPageGroup1";

        //Advanced
  
     
     }

    public class barButtonGroup
    {
        public const string ribbonItembarbuttonFont = "barButtonGroup2";
        public const string ribbonItembarbuttonParagraph = "barButtonGroup7";


    }


    public class RibbonEditItem
    { 
        public const string editItemChangeFont="changeFontNameItem1";
        public const string editItemChangeFontSize="changeFontSizeItem1";
    
    }

    public class RibbonButtonItem
    {

        public const string buttonItemStartEdit = "btnStartEditing";
        public const string buttonItemStopEdit = "btnStopEditing";
        public const string buttonItemFindJobs = "btnManualDownload";

        public const string buttonItemFinishDocument = "btnFinishDocument";
        public const string buttonItemSendtoQA = "btnSendToQA";
        public const string buttonItemSplitbyJobType = "btnSplitJobType";
        public const string buttonItemSplitybyPatint = "btnSplitPatient";

        public const string buttonItemPatients = "btnPatients";
        public const string buttonItemReferringPhysicians = "btnReferring";
        public const string buttonItemInsertMacro = "btnInsertMacro";
        public const string buttonItemAddCC = "btnCC";

        public const string buttonItemRewind = "btnRewindAudio";
        public const string buttonItemPlay = "btnPlayAudio";
        public const string buttonItemStop = "btnStopAudio";
        public const string buttonItemFastForward = "btnFastForwardAudio";
        public const string buttonItemDecreaseSpeed = "btnDecreaseSpeed";
        public const string buttonItemIncreaseSpeed = "btnIncreaseSpeed";       
              
        
        
        public const string previousAnomaly = "btnPreviousAnomaly";
        public const string nextAnomaly = "btnNextAnomaly";
    
    }

    public class SimpleTextEdit
    {
        public const string loginUser = "txtUser";
        public const string loginPassword = "txtPassword";
    
    }

    public class SimpleButton
    {
        public const string loginButton = "btnLogin";
        public const string btnSpellCancel = "btnCancel";
        public const string btnPopupSpellOk = "EditorSimpleButton[0]";
    
    }

    public class SimpleLabel
    {
        public const string lableEnvironment = "lblEnvironment";
        public const string lableError = "lblError";
    
    
    }


}
