using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;

namespace StudyDS_web.Controllers
{
    public class EventsController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public EventsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
