using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IFunctionalRequirementServices : IExcelImportable
    {
        Task<ObjectResult> GetAllFunctionalRequirementsAsync();
        Task<ObjectResult> GetByRoleOfferIdAsync(int id);
        Task<ObjectResult> UpdateOrAddFunctionalRequirementAsync
            (UpdateFunctionalRequirementConvertibleDto convertibleDto);
    }
}
