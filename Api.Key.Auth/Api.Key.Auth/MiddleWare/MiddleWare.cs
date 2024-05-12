﻿namespace Api.Key.Auth.MiddleWare
{
    public class MiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MiddleWare(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeader, out
                    var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided ");
                return;
            }
            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKey);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);
        }
    }

}
