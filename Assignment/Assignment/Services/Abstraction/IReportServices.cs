using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IReportServices
    {
        Task<ObjectResult> CreateReportAsync(CreateReportDto dto);
        Task<ObjectResult> UpdateReportAsync(UpdateReportDto dto);
        Task<ObjectResult> GetAllReportsAsync();
        Task<ObjectResult> DeleteByIdAsync(int id);
        Task<ObjectResult> GetAllOptionsAsync();
    }
}
