using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Data;
using StudyDS_web.Models;
using StudyDS_web.Models.ViewModels;

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
                    return RedirectToAction("Index", "Subjects");
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePostRegister("users", user);
                    if (response != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.TryGetValue("access_token", out var token);
            if (token != null)
            {
                HttpContext.Session.Remove("access_token");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ChangePassword()
        {
            try
            {
                var user = await webApiExecuter.InvokeGet<User>($"users/current");
                if (user != null)
                {
                    var model = new ChangePasswordViewModel
                    {
                        User = user,
                    };
                    return View(model);
                }

            }
            catch (WebApiExceptions ex)
            {
                HandleWebApiException(ex);
                return View();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePutPassword("users/updatePassword", model);
                    return RedirectToAction("Index", "Subjects");
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(model);
        }
    }
}
