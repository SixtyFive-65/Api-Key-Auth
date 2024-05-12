using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Key.AuthFilter.MiddleWare
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstans.ApiKeyHeader, out
         var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key missing");
                return;
            }
            var apiKey = _configuration.GetValue<string>(AuthConstans.ApiKey);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key missing");
                return;
            }
        }
    }

}
