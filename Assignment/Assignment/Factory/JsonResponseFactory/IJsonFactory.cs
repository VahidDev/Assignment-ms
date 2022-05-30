using Microsoft.AspNetCore.Mvc;

namespace Assignment.Factory
{
    public interface IJsonFactory
    {
        ObjectResult CreateJson
            (
            int statusCode,
            string? error = null,
            object? result = null,
            int? key = null
            );
    }
}
