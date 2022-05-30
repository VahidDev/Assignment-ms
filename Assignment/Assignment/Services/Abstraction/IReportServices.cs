using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IReportServices
    {
        Task<ObjectResult> CreateReportAsync(CreateReportDto dto);
        Task<ObjectResult> GetAllReportsAsync();
    }
}
