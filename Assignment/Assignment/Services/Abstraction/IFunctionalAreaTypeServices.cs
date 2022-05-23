using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IFunctionalAreaTypeServices
    {
        Task<JsonResult> GetAllIncludingItemsAsync();
    }
}
