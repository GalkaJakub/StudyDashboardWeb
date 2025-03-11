using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace StudyDS_web.Data
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly IConfiguration configuration;

        public AuthMiddleware(RequestDelegate requestDelegate, IConfiguration configuration)
        {
            this.requestDelegate = requestDelegate;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/Account") || context.Request.Path.StartsWithSegments("/Home"))
            {
                await requestDelegate(context);
                return;
            }

            var strToken = context.Session.GetString("access_token");

            if (string.IsNullOrEmpty(strToken))
            {
                context.Response.Redirect("/Home");
                return;
            }

            JwtToken? token = JsonConvert.DeserializeObject<JwtToken>(strToken);
            if (token == null || token.ExpiresAt < DateTime.UtcNow)
            {
                context.Response.Redirect("/Home");
                return;
            }

            await requestDelegate(context);
        }
    }
}
