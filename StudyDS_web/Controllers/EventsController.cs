using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;
using StudyDS_web.Models.ViewModels;

namespace StudyDS_web.Controllers
{
    public class EventsController : ControllerBase
    {
        private readonly IWebApiExecuter webApiExecuter;

        public EventsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        public async Task<IActionResult> Index()
        {
            var events = await webApiExecuter.InvokeGet<List<Event>>("events");
            var subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects");

            var viewModel = new EventsIndexViewModel
            {
                Events = events,
                Form = new EventFormViewModel
                {
                    Event = new Event(),
                    Subjects = subjects
                }
            };

            return View(viewModel);
        }


        public async Task<IActionResult> addEvent()
        {
            var model = new EventFormViewModel
            {
                Event = new Event(),
                Subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> addEvent(EventFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePost("events", model.Event);
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

            model.Subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects");
            return View(model);
        }


        public async Task<IActionResult> UpdateEv(int eventId)
        {
            var ev = await webApiExecuter.InvokeGet<Event>($"events/{eventId}");
            if (ev == null) return NotFound();

            var model = new EventFormViewModel
            {
                Event = ev,
                Subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects")
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("UpdateEv", model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEv(EventFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePut($"events/{model.Event.EventId}", model.Event);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException(ex);
                }
            }
            model.Subjects = await webApiExecuter.InvokeGet<List<Subject>>("subjects");
            return View(model);
        }

        public async Task<IActionResult> DeleteEv(int eventId)
        {
            await webApiExecuter.InvokeDelete<Event>($"events/{eventId}");
            return RedirectToAction(nameof(Index));
        }


    }
}