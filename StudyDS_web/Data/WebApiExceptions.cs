using System.Text.Json;

namespace StudyDS_web.Data
{
    public class WebApiExceptions: Exception
    {
        public ErrorResponse? ErrorResponse { get;}

        public WebApiExceptions(string errorJson)
        {
            ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson); ;
        }
    }
}
