using Assignment.Constants.FileConstants;

namespace Assignment.Utilities.FileUtilities
{
    public static class ExcelFileChecker
    {
        public static bool IsExcelFile(this IFormFile file)
        {
            if (file.IsFileTypeSupported(FileTypeConstants.ExcelXLSXFileContentType)
                || file.IsFileTypeSupported(FileTypeConstants.CsvFileContentType)
                || file.IsFileTypeSupported(FileTypeConstants.ExcelXLSFileContentType))
            {
                return true;
            }
                return false;
        }
    }
}
