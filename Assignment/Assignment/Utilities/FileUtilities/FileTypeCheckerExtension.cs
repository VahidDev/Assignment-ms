
namespace Assignment.Utilities.FileUtilities
{
    public static class FileTypeCheckerExtension
    {
        public static bool IsSpecificFileType
            (this IFormFile file,string extension)
        {
            if (!file.ContentType.Contains(extension))
            {
               return false;
            }
            return true;
        }
    }
}
