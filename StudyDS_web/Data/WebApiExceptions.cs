using System.Text.Json;

namespace StudyDS_web.Data
{
    // Custom exception used to handle errors returned from the Web API
    public class WebApiExceptions: Exception
    {
        public ErrorResponse? ErrorResponse { get;}

        public WebApiExceptions(string errorJson)
        {
            ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson); ;
        }
    }
}
