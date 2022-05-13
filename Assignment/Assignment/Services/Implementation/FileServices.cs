using Assignment.Services.Abstraction;
using Assignment.Utilities.RuntimeUtilities;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignment.Services.Implementation
{
    internal class FileServices : IFileServices
    {
        public ICollection<T> ReadCollectionFromExcelFile<T>(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            List<T> list = new();

            ExcelPackage package = new(stream);
            // this is necessary by the documentation 
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var worksheets = package.Workbook.Worksheets;
            foreach (var ws in worksheets)
            {
                ExcelRange excelRange = ws.Cells[ws.Dimension.Start.Row,
                    ws.Dimension.Start.Column,
                    1, ws.Dimension.End.Column];

                int totalNumberOfRows = ws.Dimension.Rows;

                IReadOnlyCollection<PropertyInfo> props = typeof(T).GetProperties();

                // all the needed props are storoed in this dict
                // Needed props have Display Attribute
                Dictionary<string, PropertyInfo> displayAttributeNameAndPropDict = new();
                foreach (PropertyInfo prop in props)
                {
                    string? name = prop.GetCustomAttribute<DisplayAttribute>()?.Name;
                    if (name == null)
                        continue;
                    displayAttributeNameAndPropDict.Add(name, prop);
                }
                int startingRow = 1;
                int startingColumn = 1;
                int headerRowNumber = 0;
                bool isFinished = false;
                // Define what is the correct row to start from

                for (int i = startingRow; i<= totalNumberOfRows; i++)
                {
                    for (int j = startingColumn;j<= excelRange.Columns; j++)
                    {
                        if (ws.Cells[i, j].Value !=null)
                        {
                            headerRowNumber = i;
                            totalNumberOfRows=totalNumberOfRows+(i-1);
                            // We take values from the row after the header row
                            startingRow = headerRowNumber + 1;
                            startingColumn = j;
                            isFinished = true;
                            break;
                        }
                    }
                    if (isFinished) break;
                }
                for (int i = startingRow; i <= totalNumberOfRows; i++)
                {
                    Dictionary<string, object> propNameAndValueDict = new();
                    for (int j = startingColumn; j <= excelRange.Columns; j++)
                    {
                        // Check if the header cell or
                        // the current cell is empty
                        if (ws.Cells[i, j].Value == null
                            ||ws.Cells[headerRowNumber, j].Value==null)
                            continue;

                        string headerCellValue = ws.Cells[headerRowNumber, j].Value.ToString();
                        if (!displayAttributeNameAndPropDict
                            .ContainsKey(headerCellValue)) continue;
                        //ws.Cells[i, j].Value is the current cell value
                        propNameAndValueDict.Add(headerCellValue, ws.Cells[i, j].Value);
                    }
                    T parentObj = Activator.CreateInstance<T>();
                    List<PropertyInfo> allProprs = parentObj
                        .GetType().GetProperties().ToList();
                    foreach (PropertyInfo prop in allProprs)
                    {
                        // ignore if prop doesn't have DisplayAttribute
                        if (prop.GetCustomAttribute<DisplayAttribute>() == null)
                            continue;
                        if (prop.IsInNamespace(nameof(DomainModels)))
                        {
                            prop.CreateCustomObjectAndSetToProperty
                                  (propNameAndValueDict, parentObj);
                        }
                        else
                        {
                            prop.SetPropertyValue<T>(parentObj
                                ,propNameAndValueDict
                                .FirstOrDefault(p => p.Key == 
                                prop.GetCustomAttribute<DisplayAttribute>().Name).Value);
                        }
                    }
                    propNameAndValueDict.Clear();
                    list.Add(parentObj);
                }
            }
            return list;
        }
    }
}
