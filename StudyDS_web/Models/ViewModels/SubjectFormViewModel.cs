using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudyDS_web.Models.ViewModels
{
    public class SubjectFormViewModel
    {
        public Subject Subject { get; set; }
        public List<SelectListItem>? SubjectTypeOptions { get; set; }
    }
}
