using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IRoleOfferServices
    {
        Task<ObjectResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
        Task<ObjectResult> ImportRoleOfferDetailsAsync(IFormFile file);
        Task<ObjectResult> GetRoleOfferAsync(int id);
        Task<ObjectResult> GetAllRoleOffersAsync();
    }
}
