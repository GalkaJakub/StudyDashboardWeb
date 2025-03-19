using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;
using StudyDS_web.Models.ViewModels;

namespace StudyDS_web.Controllers
{
    // Controller responsible for managing user data and user statistics
    public class UsersController : BaseController
    {
        private readonly IWebApiExecuter webApiExecuter;

        public UsersController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        // Display a list of all users
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<User>>("users"));
        }

        // Display current user data for editing
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

        // Save updated user data
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

        // Delete user account by ID
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

        // Generate and display user statistics
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var events = await webApiExecuter.InvokeGet<List<Event>>("events");
                var subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects");

                var totalEcts = subjects.Where(s => s.Ects.HasValue).Sum(s => s.Ects.Value);
                var passedEcts = subjects.Where(s => s.IsPassed && s.Ects.HasValue).Sum(s => s.Ects.Value);

                var subjectCount = subjects.Count;
                var subjectsPassed = subjects.Count(s => s.IsPassed);

                var eventCount = events.Count;
                var eventsPassed = events.Count(e => e.IsPassed);

                var avgSubjectGrade = subjects.Where(s => s.Grade.HasValue).DefaultIfEmpty().Average(s => s?.Grade ?? 0);
                var avgEventGrade = events.Where(e => e.Grade.HasValue).DefaultIfEmpty().Average(e => e?.Grade ?? 0);


                var model = new StatsViewModel
                {
                    TotalEcts = totalEcts,
                    PassedEcts = passedEcts,
                    SubjectCount = subjectCount,
                    SubjectsPassed = subjectsPassed,
                    EventCount = eventCount,
                    EventsPassed = eventsPassed,
                    AverageSubjectsGrade = avgSubjectGrade,
                    AverageEventsGrade = avgEventGrade,
                };

                return View(model);
            }
            catch (WebApiExceptions ex)
            {
                HandleWebApiException(ex);
                return View();
            }
        }

    }
}
