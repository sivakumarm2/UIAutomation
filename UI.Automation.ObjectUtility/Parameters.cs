using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace UI.Automation.ObjectUtility
{
    public class Parameters
    {
        #region Property
        private static string parameterDirectory = string.Empty;

        [DefaultValue(ParameterType.CSV)]
        public static ParameterType ParameterFileFormatType { get; set; }

        public static IDictionary<string, List<Parameter<string, string>>> ParameterCollection 
        {
            get 
            {
                return GetFileFormatObject().ParameterCollection;                
            }
        }

        public static string CsvParameterDirectory
        {
            get
            {
                return parameterDirectory;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) 
                    throw new DirectoryNotFoundException("Pass the parameter directory value.");

                parameterDirectory = value;

                var obj=GetFileFormatObject(); //Populating CSV file data to Object
            }
        }
        #endregion

        #region Public Method
        public static IList<Parameter<string, string>> GetParameter(string keyWord)
        {
            return GetFileFormatObject().GetParameter(keyWord);
        }

        public static Parameter<string, string> GetParameter(string keyWord,int rowIndex)
        {
            return GetFileFormatObject().GetParameter(keyWord,rowIndex);
        }
        #endregion

        #region Supporting Method
        static IParameter objParameter= null;       

        private static IParameter GetFileFormatObject()
        {
            if(ParameterFileFormatType==ParameterType.CSV)
            {
                if (objParameter == null)
                    objParameter = new CSV(CsvParameterDirectory);

                return objParameter;
            }
            return null;
        }
        #endregion
    }    
}
