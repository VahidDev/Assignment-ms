using Assignment.Factory;
using Assignment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Implementation
{
    internal class DashboardServices : IDashboardServices
    {
        private readonly IJsonFactory _jsonFactory;

        public DashboardServices(IJsonFactory jsonFactory)
        {
            _jsonFactory = jsonFactory;
        }

        public async Task<ObjectResult> GetAllInfoAsync()
        {
            return _jsonFactory.CreateJson(StatusCodes.Status200OK,null, await Task.FromResult(2));
        }
    }
}
