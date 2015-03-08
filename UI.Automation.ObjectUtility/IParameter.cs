using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UI.Automation.ObjectUtility
{
    public enum ParameterType
    {
        CSV=1
    }

    public class Parameter<T, P>
    {
        public Parameter()
        {
            _fields = new Dictionary<T, P>();
        }

        private IDictionary<T, P> _fields;

        public IDictionary<T, P> Fields
        {
            get { return _fields; }
        }
    }

    interface IParameter
    {
        IList<Parameter<string, string>> GetParameter(string automationKeyName);

        Parameter<string, string> GetParameter(string automationKeyName,int rowIndex);

        IDictionary<string, List<Parameter<string, string>>> ParameterCollection { get;}        
    }

    class CSV : IParameter
    {
        private string directory = string.Empty;

        public CSV(string csvDirectory)
        {
            if (string.IsNullOrEmpty(csvDirectory)) throw new Exception("Invalida CSV directory.");
            directory = csvDirectory;
            PopulateParameter();
        }

        public IList<Parameter<string, string>> GetParameter(string automationKeyName)
        {
            if (ParameterCollection == null || ParameterCollection.Count <= 0) return null;
            var para = ParameterCollection.FirstOrDefault(e => e.Key == string.Concat(automationKeyName, "_PARAMETER")).Value;
            if (para == null) throw new Exception(string.Format("No parameter csv file found for Key ({0}).", automationKeyName));
            return para;
        }

        public Parameter<string, string> GetParameter(string automationKeyName, int rowIndex)
        {
            var parameters = GetParameter(automationKeyName);

            if (parameters.Count < rowIndex - 1) throw new Exception("Row index not found.");

            return parameters[rowIndex - 1];
        }

        public IDictionary<string, List<Parameter<string, string>>> ParameterCollection
        {
            get;
            private set;
        }
        
        private void PopulateParameter()
        {
            try
            {
                if (!Directory.Exists(directory))
                    throw new DirectoryNotFoundException("Parameter directory is empty.");

                foreach (var file in Directory.GetFiles(directory, "*.csv"))
                {
                    var fileInfo = new FileInfo(file);
                    List<Parameter<string, string>> fileParameter = new List<Parameter<string, string>>();
                    var csvRows = File.ReadAllLines(file);
                    if (csvRows.Length > 0)
                    {
                        string csvColumnRow = csvRows[0];
                        for (int rowCount = 1; rowCount <= csvRows.Length - 1; rowCount++)
                        {
                            var columnData = csvColumnRow.Split(',');
                            var rowData = csvRows[rowCount].Split(',');

                            if (columnData.Length != rowData.Length) break;

                            Parameter<string, string> parameter = new Parameter<string, string>();
                            for (int colCount = 0; colCount < columnData.Length; colCount++)
                            {
                                if (!string.IsNullOrEmpty(columnData[colCount]))
                                    parameter.Fields.Add(columnData[colCount], rowData[colCount]);
                            }
                            if (parameter.Fields.Count > 0) fileParameter.Add(parameter);
                        }
                        if (ParameterCollection == null) ParameterCollection = new Dictionary<string, List<Parameter<string, string>>>();
                        ParameterCollection.Add(fileInfo.Name.Replace(fileInfo.Extension, ""), fileParameter);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
