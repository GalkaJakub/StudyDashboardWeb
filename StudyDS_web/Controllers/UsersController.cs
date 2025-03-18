using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;
using StudyDS_web.Models.ViewModels;

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

        public async Task<IActionResult> UpdateUser()
        {
            try
            {
                var user = await webApiExecuter.InvokeGet<User>($"users/current");
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
                    return RedirectToAction(nameof(Index), "subjects");
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

        public IActionResult GetStats()
        {
            //ilość ects, srednia ocen, ile przedmiotów, ile eventów
            var model = new StatsViewModel();
            return View(model);
        }

    }
}
