using Microsoft.AspNetCore.Mvc;

namespace Assignment.Factory
{
    internal class JsonFactory : IJsonFactory
    {
        public JsonResult CreateJson(int? response, object? data=null)
        {
            JsonResult jsonResult = new(data);
            jsonResult.StatusCode = response;
            jsonResult.ContentType = "application/json";
            return jsonResult;
        }
    }
}
