using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IDashboardServices
    {
        Task<JsonResult> GetAllInfoAsync(); 
    }
}
