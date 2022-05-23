using Assignment.Constants.FileConstants;

namespace Assignment.Utilities.FileUtilities
{
    public static class ExcelFileChecker
    {
        public static bool IsExcelFile(this IFormFile file)
        {
            if (file.IsSpecificFileType(FileTypeConstants.ExcelXLSXFileContentType)
                || file.IsSpecificFileType(FileTypeConstants.CsvFileContentType)
                || file.IsSpecificFileType(FileTypeConstants.ExcelXLSFileContentType))
            {
                return true;
            }
                return false;
        }
    }
}
