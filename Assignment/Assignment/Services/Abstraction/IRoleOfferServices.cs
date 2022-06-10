using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IRoleOfferServices : IExcelImportable
    {
        Task<ObjectResult> ImportRoleOfferDetailsAsync(IFormFile file);
        Task<ObjectResult> GetRoleOfferAsync(int id);
        Task<ObjectResult> GetAllRoleOffersAsync();
        string? Email { get; set; }
    }
}
