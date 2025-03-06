using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;

namespace StudyDS_web.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public SubjectsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<Subject>>("subjects"));
        }
    }
}
