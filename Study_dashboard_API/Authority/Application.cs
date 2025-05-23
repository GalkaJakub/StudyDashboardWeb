﻿namespace Study_dashboard_API.Authority
{
    // Represents a client application registered in the system
    public class Application
    {
        public int ApplicationId { get; set; }
        public string? ApplicationName { get; set; }
        public string? ClientId { get; set; }
        public string? Secret { get; set; }
        public string? Scopes { get; set;}

    }
}
