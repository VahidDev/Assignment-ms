using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    internal interface IExcelImportable
    {
        public Task<ObjectResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
    }
}
