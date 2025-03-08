using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;

namespace StudyDS_web.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IWebApiExecuter webApiExecuter;

        public UsersController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<User>>("users"));
        }

        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(User user)
        {   
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePost("users", user);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> UpdateUser(int userId)
        {
            try
            {
                var user = await webApiExecuter.InvokeGet<User>($"users/{userId}");
                if (user != null)
                {
                    return View(user);
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
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePut($"users/{user.UserId}", user);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await webApiExecuter.InvokeDelete<User>($"users/{userId}");
                return RedirectToAction(nameof(Index));
            }
            catch (WebApiExceptions ex)
            {
                HandleWebApiException(ex);
                return View(nameof(Index), await webApiExecuter.InvokeGet<List<User>>("users"));
            }
        }
    }
}
