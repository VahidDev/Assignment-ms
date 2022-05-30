using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IFunctionalAreaTypeServices
    {
        Task<ObjectResult> GetAllIncludingItemsAsync();
    }
}
