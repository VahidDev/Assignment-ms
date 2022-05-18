using Microsoft.AspNetCore.Mvc;

namespace Assignment.Factory
{
    public interface IJsonFactory
    {
        JsonResult CreateJson(int? statusCode, object data=null);
    }
}
