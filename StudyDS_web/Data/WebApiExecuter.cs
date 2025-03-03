using Azure;
using System.Text.Json;

namespace StudyDS_web.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private const string apiName = "StudyApi";
        private readonly IHttpClientFactory httpClientFactory;

        public WebApiExecuter(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        // wysyła get i zwraca w formie Json, konwertujac na T
        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);

            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }
        // Samo Task czyli tylko wysyła 
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            await HandleError(response);
        }

        public async Task InvokeDelete<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
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
    }
}
