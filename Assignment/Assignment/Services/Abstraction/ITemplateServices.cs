using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface ITemplateServices
    {
        Task<JsonResult> CreateAsync(CreateTemplateDto templates);
        Task<JsonResult> UpdateAsync(UpdateTemplateDto templateDto);
        Task<JsonResult> DeleteAsync(int id);
        Task<JsonResult> GetAllTemplatesAsync();
    }
}
