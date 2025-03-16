using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudyDS_web.Models.ViewModels
{
    public class EventFormViewModel
    {
        public Event Event { get; set; }
        public List<Subject>? Subjects { get; set; }
        public List<SelectListItem>? EventTypeOptions { get; set; }

    }
}
