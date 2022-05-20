using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IFunctionalRequirementServices
    {
        Task<JsonResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
        Task<JsonResult> GetAllFunctionalRequirementssync();
    }
}
