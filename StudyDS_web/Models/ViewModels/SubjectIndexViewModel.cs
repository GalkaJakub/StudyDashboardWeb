using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudyDS_web.Models.ViewModels
{
    public class SubjectIndexViewModel
    {
        public List<Subject> Subjects { get; set; }
        public SubjectFormViewModel formViewModel { get; set; }
    }
}
