using Azure;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace StudyDS_web.Data
{
    // Service for executing HTTP requests to the Web API
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

        // Sends GET request to API and returns deserialized response
        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            AddJwtToHeader(httpClient);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Sends POST request with JSON body and returns deserialized response
        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            AddJwtToHeader(httpClient);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Sends PUT request with JSON body
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            AddJwtToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            await HandleError(response);

        }

        // Sends DELETE request
        public async Task InvokeDelete<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            AddJwtToHeader(httpClient);
            var response = await httpClient.DeleteAsync(relativeUrl);
            await HandleError(response);
        }

        // Handles error responses by throwing a custom exception   
        private async Task HandleError(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorJson = await httpResponse.Content.ReadAsStringAsync();
                throw new WebApiExceptions(errorJson);
            }
        }

        // Sends POST request to register a new user
        public async Task<T?> InvokePostRegister<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Sends PUT request to update user password
        public async Task InvokePutPassword<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            AddJwtToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
        }

        // Authenticates user and stores JWT token in session
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

        // Adds JWT token to Authorization header for API requests
        private void AddJwtToHeader(HttpClient httpClient)
        {
            string? strToken = httpContextAccessor.HttpContext?.Session.GetString("access_token");
            JwtToken? token = JsonConvert.DeserializeObject<JwtToken>(strToken);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
    }
}
