using Assignment.Constants.FileConstants;
using Assignment.Services.Abstraction;
using Assignment.Utilities.FileUtilities;
using Assignment.Utilities.RuntimeUtilities;
using ExcelDataReader;
using System.Reflection;

namespace Assignment.Services.Implementation
{
    internal class FileServices : IFileServices
    {
        private readonly IRuntimeServices _runtimeServices;

        public FileServices(IRuntimeServices runtimeServices)
        {
            _runtimeServices = runtimeServices;
        }

        public ICollection<T>? ReadCollectionFromExcelFile<T>(IFormFile file)
        {
            List<T> items = new();

            IReadOnlyCollection<PropertyInfo> props = typeof(T).GetProperties();

            // all the needed props will be storoed in this dict
            // Needed props have Display Attribute
            Dictionary<string, PropertyInfo> displayAttributeNameAndPropDict
                = DisplayAttributeAndPropDictGenerator
                .CreateDict(props, new Dictionary<string, PropertyInfo>());

            //This needs to be set (from the documentation)
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            Stream stream = file.OpenReadStream();
            IExcelDataReader reader;

            // Determine which reader to use. For csv it is Csv reader
            // but for other extensions(xlsx,xls) it is just XML reader 
            if (file.IsSpecificFileType(FileTypeConstants.CsvFileContentType))
            {
                reader = ExcelReaderFactory.CreateCsvReader(stream);
            }
            else
            {
                reader=ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            // The below dictionary will consist of all the headers 
            // with their indecies (column index)
            Dictionary<int,string> headersDict = new ();
            bool IsStartingRowFound = false;
            int startingColumn = 0;
            bool isStartingColumnFound = false;

            // Find the starting row and the starting column
            while (!IsStartingRowFound)
            {
                reader.Read();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (reader[i] != null)
                    {
                        string? headerValue = reader[i].ToString();
                        if (headerValue != null)
                        {
                            headersDict.Add(i,headerValue);
                            if (!isStartingColumnFound)
                            {
                                startingColumn = i;
                                isStartingColumnFound = true;
                            }
                        }
                    }
                }
                if(headersDict.Count > 0) 
                    IsStartingRowFound=true;
            }

            while (reader.Read())
            {
                Dictionary<string, object> propNameAndValueDict = new();
                for (int j = startingColumn; j < reader.FieldCount; j++)
                {
                    // Check if the header cell or
                    // the current cell is empty or an empty string
                    if (reader.GetValue(j) == null
                        || string.IsNullOrEmpty(reader.GetValue(j).ToString()))
                        continue;
                   
                    if (!headersDict.ContainsKey(j))
                        continue;
                    string headerCellValue = headersDict[j].Trim().ToLower();

                    if (!displayAttributeNameAndPropDict
                        .ContainsKey(headerCellValue)) continue;

                    //ws.Cells[i, j].Value is the current cell value
                    propNameAndValueDict.Add(headerCellValue, reader.GetValue(j));
                }
                if (propNameAndValueDict.Count == 0) continue;
                
                if(propNameAndValueDict.Count != displayAttributeNameAndPropDict.Count)
                {
                    return null;
                }
                
                if (propNameAndValueDict.Count == headersDict.Count) 
                {
                    items.Add(_runtimeServices.CreateCustomObject<T>(propNameAndValueDict));

                    // Reset dict 
                    propNameAndValueDict.Clear();
                }
                else
                {
                    return null;
                }
            }
            return items;
        }
    }
}
