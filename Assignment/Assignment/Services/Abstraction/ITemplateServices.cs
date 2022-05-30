using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface ITemplateServices
    {
        Task<ObjectResult> CreateAsync(CreateTemplateDto templates);
        Task<ObjectResult> UpdateAsync(UpdateTemplateDto templateDto);
        Task<ObjectResult> DeleteAsync(int id);
        Task<ObjectResult> GetAllTemplatesAsync();
    }
}
