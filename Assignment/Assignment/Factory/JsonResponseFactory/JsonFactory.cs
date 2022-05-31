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
            object? value = null, 
            int? key = null
            )
        {
            return new ObjectResult(
                new CustomJson { Error = error, Key = key, Value = value }
                ) { StatusCode = statusCode };
        }
    }
}
