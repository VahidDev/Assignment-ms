using Assignment.Factory.JsonResponseFactory;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Factory
{
    internal class JsonFactory : IJsonFactory
    {
        public ObjectResult CreateJson
            (
            int statusCode, 
            string? error = null,
            object? result = null, 
            int? key = null
            )
        {
            return new ObjectResult(
                new CustomJson { Error = error, Key = key, Result = result }
                ) { StatusCode = statusCode };
        }
    }
}
