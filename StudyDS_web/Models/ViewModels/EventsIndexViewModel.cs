using Study_dashboard_API.Models;

namespace StudyDS_web.Models.ViewModels
{
    public class EventsIndexViewModel
    {
        public List<Event> Events { get; set; }
        public EventFormViewModel Form { get; set; }
    }
}
