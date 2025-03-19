namespace Study_dashboard_API.Authority
{
    public static class AppRepository
    {
        // A static repository holding information about registered applications
        private static List<Application> _applications = new List<Application>()
        {
            new Application()
            {
                ApplicationId = 1,
                ApplicationName = "MVCWebApp",
                ClientId = "699e612e-cec2-48ac-b17c-41fd785c11a4",
                Secret = "8B4E218E-0B39-4F2C-8AE7-28E02F3C275C",
                Scopes = "read,write"
            }
        };

        public static Application? GetApplication(string clientId)
        {
            return _applications.FirstOrDefault(x => x.ClientId == clientId);
        }
    }
}
