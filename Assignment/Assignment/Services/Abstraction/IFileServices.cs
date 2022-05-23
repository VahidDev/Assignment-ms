namespace Assignment.Services.Abstraction
{
    public interface IFileServices
    {
        ICollection<T>? ReadCollectionFromExcelFile<T>(IFormFile file);
    }
}
