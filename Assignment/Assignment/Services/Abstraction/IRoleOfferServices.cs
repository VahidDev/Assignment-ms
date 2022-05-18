using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IRoleOfferServices
    {
        Task<JsonResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
        Task<JsonResult> GetRoleOfferAsync(int id);
        Task<JsonResult> GetALlRoleOffersAsync();
        Task<JsonResult> GetALlNestedRoleOffersAsync();
    }
}
