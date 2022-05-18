using Microsoft.AspNetCore.Mvc;

namespace Assignment.Utilities.ResponseUtilities
{
    public static class ResponseGenerator
    {
        public static ObjectResult GetResponse(JsonResult json)
        {
            return new ObjectResult (json){ StatusCode=json.StatusCode};
        }
    }
}
