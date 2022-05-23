using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    internal interface IExcelImportable
    {
        public Task<JsonResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file);
    }
}
