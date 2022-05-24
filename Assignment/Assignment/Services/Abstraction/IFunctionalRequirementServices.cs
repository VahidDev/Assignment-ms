using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IFunctionalRequirementServices
    {
        Task<JsonResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
        Task<JsonResult> GetAllFunctionalRequirementsAsync();
        Task<JsonResult> GetByRoleOfferIdAsync(int id);
        Task<JsonResult> UpdateOrAddFunctionalRequirementAsync(UpdateFunctionalRequirementConvertibleDto convertibleDto);
    }
}
