using Azure;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace StudyDS_web.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private const string apiName = "StudyApi";
        private const string authApiName = "AuthorityApi";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WebApiExecuter(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }
        // wysyła get i zwraca w formie Json, konwertujac na T
        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            try
            {
                AddJwtToHeader(httpClient);
            }
            catch (UnauthorizedAccessException)
            {
                httpContextAccessor.HttpContext.Response.Redirect("Account/Login");
                return default;
            }

            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            try
            {
                AddJwtToHeader(httpClient);
            }
            catch (UnauthorizedAccessException)
            {
                httpContextAccessor.HttpContext.Response.Redirect("Account/Login");
                return default;
            }

            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }
        // Samo Task czyli tylko wysyła 
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            try
            {
                AddJwtToHeader(httpClient);
            }
            catch (UnauthorizedAccessException)
            {
                httpContextAccessor.HttpContext.Response.Redirect("Account/Login");
                return;
            }

            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
        }

        public async Task InvokeDelete<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);

            try
            {
                AddJwtToHeader(httpClient);
            }
            catch (UnauthorizedAccessException)
            {
                httpContextAccessor.HttpContext.Response.Redirect("Account/Login");
                return;
            }
            var response = await httpClient.DeleteAsync(relativeUrl);
            await HandleError(response);
        }

        private async Task HandleError(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorJson = await httpResponse.Content.ReadAsStringAsync();
                throw new WebApiExceptions(errorJson);
            }
        }

        public async Task<T?> InvokePostRegister<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokeLogin(string? login, string? password)
        {

            var clientId = configuration.GetValue<string>("ClientId");
            var secret = configuration.GetValue<string>("Secret");

            //Authenticate
            var authClient = httpClientFactory.CreateClient(authApiName);
            var response = await authClient.PostAsJsonAsync("auth", new AppCredencial
            {
                ClientId = clientId,
                Secret = secret,
                UserName = login,
                UserPassword = password
            });

            await HandleError(response);
            //Get JWT
            string strToken = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<JwtToken>(strToken);
            httpContextAccessor.HttpContext?.Session.SetString("access_token", strToken);
        }

        private void AddJwtToHeader(HttpClient httpClient)
        {
            string? strToken = httpContextAccessor.HttpContext?.Session.GetString("access_token");
            if (!string.IsNullOrEmpty(strToken))
            {
                JwtToken? token = JsonConvert.DeserializeObject<JwtToken>(strToken);
                if (token != null && token.ExpiresAt > DateTime.UtcNow)
                {
                    //Pass JWT to endpoints
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
                    return;
                }
            }
            throw new UnauthorizedAccessException("JWT token is missing or expired.");
        }
    }
}
