using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace UI.Automation.Win.ObjectRepository
{
    public static class CommonVariables
    {
        static CommonVariables()
        {
            CommonVariables.ApplictionUnderTest = ConfigurationManager.AppSettings["ApplictionUnderTest"];
            CommonVariables.TestCasePath = ConfigurationManager.AppSettings["TestCasePath"];
            CommonVariables.ClearDownPickingList = ConfigurationManager.AppSettings["ClearDownPickingList"];
            CommonVariables.CaptureScreen = ConfigurationManager.AppSettings["CaptureScreen"];
            CommonVariables.sqlscripts = ConfigurationManager.AppSettings["sqlscripts"];
            CommonVariables.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            CommonVariables.ContactConnectionString = ConfigurationManager.AppSettings["ContactConnectionString"];
            CommonVariables.SDMConnectionString = ConfigurationManager.AppSettings["SDMConnectionString"];
            CommonVariables.StoreDocConnectionString = ConfigurationManager.AppSettings["StoreDocConnectionString"];
            CommonVariables.DeliveryDbConnectionString = ConfigurationManager.AppSettings["DeliveryDbConnectionString"];
            CommonVariables.MartiniDbConnectionString = ConfigurationManager.AppSettings["MartiniDbConnectionString"];
            CommonVariables.IsNPDIGHS = ConfigurationManager.AppSettings["IsNPDIGHS"];
            // CommonVariables.IsNPDIGHS = ConfigurationManager.AppSettings["IsManualAudit"];
            CommonVariables.IsManualAudit = ConfigurationManager.AppSettings["IsManualAudit"];
            CommonVariables.AppExe = ConfigurationManager.AppSettings["AppExe"];
            CommonVariables.ManualAuditExe = ConfigurationManager.AppSettings["ManualAuditExe"];
            CommonVariables.ScheduleOrderFilePath = ConfigurationManager.AppSettings["ScheduleOrderFilePath"];
            CommonVariables.CoveragePath = ConfigurationManager.AppSettings["CoveragePath"];
            CommonVariables.coveragepathVS2010 = ConfigurationManager.AppSettings["coveragepathVS2010"];
            CommonVariables.Logspath = ConfigurationManager.AppSettings["Logspath"];
            CommonVariables.CoverageReports = ConfigurationManager.AppSettings["CoverageReports"];
            CommonVariables.ProductsFile = ConfigurationManager.AppSettings["ProductsFile"];
            CommonVariables.ORSFilePath = ConfigurationManager.AppSettings["ORSFilePath"];
            CommonVariables.TeampadExe = ConfigurationManager.AppSettings["TeampadExe"];
            CommonVariables.ResultXmlPath = ConfigurationManager.AppSettings["ResultXmlPath"];
            CommonVariables.ResultXmlName = ConfigurationManager.AppSettings["ResultXmlName"];
            CommonVariables.ScanTrayDigit = ConfigurationManager.AppSettings["ScanTrayDigit"];
            CommonVariables.EnableCoverage = ConfigurationManager.AppSettings["EnableCoverage"];
            //RTDP PATHS.
            CommonVariables.ExpectedCDNPath = ConfigurationManager.AppSettings["ExpectedCDNPath"];
            CommonVariables.ActualCDNPath = ConfigurationManager.AppSettings["ActualCDNPath"];
            CommonVariables.IsExpectedCDNXmlsGenerated = ConfigurationManager.AppSettings["IsExpectedCDNXmlsGenerated"];
            CommonVariables.CdnPath = ConfigurationManager.AppSettings["SrcCDNpath"];
            CommonVariables.CdnBackupPath = ConfigurationManager.AppSettings["DestCDNpath"];
            CommonVariables.waitTime = ConfigurationManager.AppSettings["TimeDelay"];
            //Added for TestCase Automation
            CommonVariables.TestConnectionString = ConfigurationManager.AppSettings["TestConnectionString"];
            CommonVariables.TestType = ConfigurationManager.AppSettings["DataBaseExecution"];

            CommonVariables.ScanTrayDigit = ConfigurationManager.AppSettings["ScanTrayDigit"];
            CommonVariables.EnableCoverage = ConfigurationManager.AppSettings["EnableCoverage"];
            CommonVariables.IGHSUserManagementConnectionString = ConfigurationManager.AppSettings["IGHSUserManagementConnectionString"];
            CommonVariables.IGHSPickingControlConnectionString = ConfigurationManager.AppSettings["IGHSPickingControlConnectionString"];
            CommonVariables.IGHSPCSOrderBrokerConnectionString = ConfigurationManager.AppSettings["IGHSPCSOrderBrokerConnectionString"];
            CommonVariables.IGHSPCSConfigRConnectionString = ConfigurationManager.AppSettings["IGHSPCSConfigRConnectionString"];
            CommonVariables.IGHSPCSAdminConnectionString = ConfigurationManager.AppSettings["IGHSPCSAdminConnectionString"];
            CommonVariables.IGHSLabelPrintingConnectionString = ConfigurationManager.AppSettings["IGHSLabelPrintingConnectionString"];
            CommonVariables.IGHSDeliverySchedulerConnectionString = ConfigurationManager.AppSettings["IGHSDeliverySchedulerConnectionString"];
            CommonVariables.IGHSAppstoreQueueConnectionString = ConfigurationManager.AppSettings["IGHSAppstoreQueueConnectionString"];
            CommonVariables.PcsAdminUrl = ConfigurationManager.AppSettings["PcsAdminUrl"];
            CommonVariables.sTxtFilePath = CommonVariables.Logspath;
            string NoOfOrders = ConfigurationManager.AppSettings["NoOfOrders"];
            CommonVariables._NoOfOrders = Convert.ToInt32(NoOfOrders);
            CommonVariables.ActualXmlReports = ConfigurationManager.AppSettings["ActualXmlReports"];
            CommonVariables.EnableProfiling = ConfigurationManager.AppSettings["EnableProfiling"];
            CommonVariables.ProfileReports = ConfigurationManager.AppSettings["ProfileReports"];
            CommonVariables.ProfileWebAppReports = ConfigurationManager.AppSettings["ProfileWebAppReports"];
            CommonVariables.ProfilingType = ConfigurationManager.AppSettings["ProfilingType"];
            CommonVariables.PickerId = ConfigurationManager.AppSettings["BadgeId"];
            // Added for Checkout Automation
            CommonVariables.StrCheckoutSourceConn = ConfigurationManager.AppSettings["StrCheckoutSourceConn"];
            CommonVariables.E2EPath = ConfigurationManager.AppSettings["E2EPath"];

            //Added For Instrumentation Paths
            CommonVariables.EnableInstrumentation = ConfigurationManager.AppSettings["EnableInstrumentation"];
            CommonVariables.PdbFilesSourcePath = ConfigurationManager.AppSettings["PdbFilesSourcePath"];
            CommonVariables.WinServicesPath = ConfigurationManager.AppSettings["WinServicesPath"];
            CommonVariables.WebServicesPath = ConfigurationManager.AppSettings["WebServicesPath"];
            CommonVariables.OtherAppsPath = ConfigurationManager.AppSettings["OtherAppsPath"];
            //DMS format for Estate and Dcos Store
            CommonVariables.DMSFormat = ConfigurationManager.AppSettings["DMSFormat"];

            CommonVariables.ReachClientPath = ConfigurationManager.AppSettings["ReachClientPath"];
            CommonVariables.PrePicking = ConfigurationManager.AppSettings["PrePicking"];
            CommonVariables.ReachClientFile = ConfigurationManager.AppSettings["ReachClientFile"];
            CommonVariables.NPDLanguage = ConfigurationManager.AppSettings["NPDLanguage"];

            CommonVariables.QueryLocation = ConfigurationManager.AppSettings["QueryLocation"];


            CommonVariables.AutomationNameSpace = ConfigurationManager.AppSettings["AutomationNameSpace"];
            CommonVariables.TestCaseParameterPath = ConfigurationManager.AppSettings["TestCaseParameterPath"];

        }

        public static string ApplictionUnderTest = "Dcos";
        public static string TestCasePath;
        public static string ClearDownPickingList;
        public static string CaptureScreen;
        public static string IsManualAudit;
        public static string sqlscripts;
        public static string ConnectionString;
        public static string StoreDocConnectionString;
        public static string DeliveryDbConnectionString;
        public static string MartiniDbConnectionString;
        public static string AppExe;
        public static string ScheduleOrderFilePath;
        public static string CoveragePath;
        public static string coveragepathVS2010;
        public static string Logspath;
        public static string CoverageReports;
        public static string ProductsFile;
        public static string ORSFilePath;
        public static string TeampadExe;
        public static string ResultXmlPath;
        public static string ResultXmlName;
        public static string ExpectedCDNPath;
        public static string ActualCDNPath;
        public static string IsExpectedCDNXmlsGenerated;
        public static string ManualAuditExe;
        public static string IsNPDIGHS;
        //Added for Testing DBAutomation 
        public static string TestConnectionString;
        public static string TestType;
        public static string TrolleyConnectionString;
        public static string IsTrolley;
        /*---------------------------------------*/

        public static string CdnPath;
        public static string CdnBackupPath;
        public static string waitTime;
        public static string ScanTrayDigit;
        public static string isPickingFromFile;
        public static string EnableCoverage;
        public static string EnableProfiling;
        public static string ProfileReports;
        public static string ProfileWebAppReports;
        public static string ProfilingType;
        public static string E2EPath;

        public static Int32 _NoOfOrders;
        public static string OrderIds;
        public static string TillServicesName;
        public static string WinposServicesName;
        public static string IGHSUserManagementConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=UserManagement;Integrated Security=True";
        public static string IGHSPickingControlConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=SK_PickingControl;Integrated Security=True";
        public static string IGHSPCSOrderBrokerConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=PCSOrderBroker;Integrated Security=True";
        public static string IGHSPCSConfigRConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=PCSConfig;Integrated Security=True";
        public static string IGHSPCSAdminConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=PCSAdmin;Integrated Security=True";
        public static string IGHSLabelPrintingConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=LabelPrinting;Integrated Security=True";
        public static string IGHSDeliverySchedulerConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=DeliveryScheduler;Integrated Security=True";
        public static string IGHSAppstoreQueueConnectionString = @"Data Source=VDI01T8AHSC446;Initial Catalog=AppstoreQueue;Integrated Security=True";
        public static string PcsAdminUrl;

        public static string ActualXmlReports;
        public static string StrCheckoutSourceConn;

        public static string EnableInstrumentation;
        public static string PdbFilesSourcePath;
        public static string WinServicesPath;
        public static string WebServicesPath;
        public static string OtherAppsPath;
        public static string ContactConnectionString;
        public static string SDMConnectionString;

        public static string PresentCscFilePath;
        public static bool SanityCheck = true;
        public static string DMSFormat;
        public static string TestCaseParameterPath;
        public static string sComExceptionlog;
        public static string sPlaybackExceptionlog;
        public static string[] VanTripsBeforeScheduling = new string[12];
        public static string[] VanTripsAfterScheduling = new string[12];
        public static string[] VanId = new string[100];
        public static string[] VanTripID = new string[100];
        public static string[] FriendlyVanName = new string[100];
        public static string[] Orders = new string[100];
        public static string[] NormalOrders = new string[100];
        public static string[] PodOrders = new string[100];
        public static string[] NonPodOrders = new string[100];
        public static string FriendlyVanTripForVehicleForPod;
        public static string[] VanTripsForPODStoreOrders = new string[100];
        public static string[] VanTripsForNonPODStoreOrders = new string[100];
        public static string FriendlyVanTripForNonPodVan1;
        public static string FriendlyVanTripForNonPodVan2;
        public static string FriednlyVanTripForUnRoutedOrders;
        public static string LatestShortOrdNum;
        public static string[] ShortOrdNums = new string[100];
        public static string PickerId;
        public static string ProductId;
        public static string ReachClientPath;
        public static string PrePicking;
        public static string ReachClientFile;
        public static string NPDLanguage;
        public static string AutomationNameSpace;
        public static string QueryLocation;
        public static string sTxtFilePath;

    }
}
