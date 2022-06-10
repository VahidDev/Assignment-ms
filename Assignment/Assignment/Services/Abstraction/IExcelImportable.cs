using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IExcelImportable
    {
        Task<ObjectResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
    }
}
