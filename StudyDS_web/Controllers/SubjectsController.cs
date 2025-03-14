﻿using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;
using Microsoft.AspNetCore.Authorization;

namespace StudyDS_web.Controllers
{
    public class SubjectsController : ControllerBase
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

        public async Task<IActionResult> DeleteSub(int subjectId)
        {
            await webApiExecuter.InvokeDelete<Subject>($"subjects/{subjectId}");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateSub(int subjectId)
        {
            try
            {
                var subject = await webApiExecuter.InvokeGet<Subject>($"subjects/{subjectId}");
                if (subject != null)
                {
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        return PartialView("UpdateSub", subject);

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



    }
}
