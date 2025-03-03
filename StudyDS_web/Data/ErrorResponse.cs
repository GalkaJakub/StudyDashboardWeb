using System.Text.Json.Serialization;

namespace StudyDS_web.Data
{
    public class ErrorResponse
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("errors")]
        public Dictionary<string, List<String>>? Errors { get; set; }
    }
}
