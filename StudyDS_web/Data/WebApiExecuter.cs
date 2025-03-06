using Azure;
using Newtonsoft.Json;
using System.Text.Json;
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
            await AddJwtToHeader(httpClient);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }
        // Samo Task czyli tylko wysyła 
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
        }

        public async Task InvokeDelete<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
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

        private async Task AddJwtToHeader(HttpClient httpClient)
        {
            JwtToken? token = null;
            string? strToken = httpContextAccessor.HttpContext?.Session.GetString("access_token");
            if (!string.IsNullOrEmpty(strToken))
            {
                token = JsonConvert.DeserializeObject<JwtToken>(strToken);
            }

            if (token == null || token.ExpiresAt <= DateTime.UtcNow)
            {
                var clientId = configuration.GetValue<string>("ClientId");
                var secret = configuration.GetValue<string>("Secret");

                //Authenticate
                var authClient = httpClientFactory.CreateClient(authApiName);
                var response = await authClient.PostAsJsonAsync("auth", new AppCredencial
                {
                    ClientId = clientId,
                    Secret = secret
                });

                response.EnsureSuccessStatusCode();
                //Get JWT
                strToken = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<JwtToken>(strToken);
                httpContextAccessor.HttpContext?.Session.SetString("access_token", strToken);
            }
            //Pass JWT to endpoints
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
    }
}
