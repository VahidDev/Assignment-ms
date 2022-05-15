using DomainModels.Dtos;

namespace Assignment.Services.Abstraction
{
    public interface ITemplateServices
    {
        Task<bool> CreateAsync(IReadOnlyCollection<CreateTemplateDto>templates);
        Task<bool> UpdateAsync(UpdateTemplateDto templateDto);
    }
}
