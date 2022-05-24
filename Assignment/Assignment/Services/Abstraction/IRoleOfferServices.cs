using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IRoleOfferServices
    {
        Task<JsonResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
        Task<JsonResult> ImportRoleOfferDetailsAsync(IFormFile file);
        Task<JsonResult> GetRoleOfferAsync(int id);
        Task<JsonResult> GetAllRoleOffersAsync();
    }
}
