using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Data;
using StudyDS_web.Models;

namespace StudyDS_web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IWebApiExecuter webApiExecuter;

        public AccountController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokeLogin(user.Name, user.Password);
                    return RedirectToAction("Index", "subjects");
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(user);
        }
    }
}
