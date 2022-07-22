using Assignment.Constants.RoutingConstants;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class DynamicRouteAttribute : RouteAttribute
    {
        public DynamicRouteAttribute(string template) 
            : base($"{RoutingConstants.DynamicRoute}/{template}")
        {
            ArgumentNullException.ThrowIfNull(template);
        }
    }
}
