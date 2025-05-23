﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace StudyDS_web.Data
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
        [JsonProperty("user_id")]
        public string? userId { get; set; }
    }
}
