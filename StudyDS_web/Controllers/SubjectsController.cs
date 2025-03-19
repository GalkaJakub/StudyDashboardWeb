using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;
using StudyDS_web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace StudyDS_web.Controllers
{
    // Controller responsible for handling subjects
    public class SubjectsController : BaseController
    {
        private readonly IWebApiExecuter webApiExecuter;

        public SubjectsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        // Display list of subjects + form to add a new one
        public async Task<IActionResult> Index()
        {
            var subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects");

            var model = new SubjectIndexViewModel
            {
                Subjects = subjects,
                formViewModel = new SubjectFormViewModel
                {
                    Subject = new Subject()
                }
            };

            model.formViewModel.SubjectTypeOptions = Enum.GetValues(typeof(PassingType))
            .Cast<PassingType>()
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.GetType()
                        .GetMember(e.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? e.ToString()
            })
            .ToList();

            return View(model);
        }

        // Create a new subject
        [HttpPost]
        public async Task<IActionResult> addSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePost("subjects", subject);
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
            var subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects");
            return View("Index", subjects);
        }

        // Delete selected subject
        public async Task<IActionResult> DeleteSub(int subjectId)
        {
            await webApiExecuter.InvokeDelete<Subject>($"subjects/{subjectId}");
            return RedirectToAction(nameof(Index));
        }
        // Load subject into modal for editing  
        public async Task<IActionResult> UpdateSub(int subjectId)
        {
            try
            {
                var subject = await webApiExecuter.InvokeGet<Subject>($"subjects/{subjectId}");
                if (subject != null)
                {
                    var model = new SubjectFormViewModel
                    {
                        Subject = subject,
                    };

                    model.SubjectTypeOptions = Enum.GetValues(typeof(PassingType))
                    .Cast<PassingType>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.GetType()
                                .GetMember(e.ToString())
                                .First()
                                .GetCustomAttribute<DisplayAttribute>()?.Name ?? e.ToString()
                    })
                    .ToList();

                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        return PartialView("UpdateSub", model);

                    return View(subject);
                }
            }
            catch (WebApiExceptions ex)
            {
                HandleWebApiException(ex);
                return View();
            }

            return NotFound();
        }

        // Save updated subject
        [HttpPost]
        public async Task<IActionResult> UpdateSub(Subject subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePut($"subjects/{subject.SubjectId}", subject);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException (ex);
                }
            }
            return View(subject);
        }

        // Load modal for setting grade/pass status
        public async Task<IActionResult> PassSub(int subjectId)
        {
            try
            {
                var subject = await webApiExecuter.InvokeGet<Subject>($"subjects/{subjectId}");
                if (subject != null)
                {
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        return PartialView("UpdatePassSub", subject);

                    return View(subject);
                }
            }
            catch (WebApiExceptions ex)
            {
                HandleWebApiException(ex);
                return View();
            }

            return NotFound();
        }

    }
}
