
namespace Assignment.Services.Abstraction
{
    public interface IRoleOfferServices
    {
        Task<string> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
    }
}
