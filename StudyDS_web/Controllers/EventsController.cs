using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Models;
using StudyDS_web.Data;

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
            return View(await webApiExecuter.InvokeGet<List<Event>>("events")); 
        }

        public IActionResult addEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addEvent(Event ev)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePost("events", ev);
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
            return View(ev);
        }

        public async Task<IActionResult> UpdateEv(int eventId)
        {
            try
            {
                var ev = await webApiExecuter.InvokeGet<Event>($"events/{eventId}");
                if (ev != null)
                {
                    return View(ev);
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
        public async Task<IActionResult> UpdateEv(Event ev)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePut($"events/{ev.EventId}", ev);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiExceptions ex)
                {
                    HandleWebApiException (ex);
                }
            }
            return View();
        }

        public async Task<IActionResult> DeleteEv(int eventId)
        {
            await webApiExecuter.InvokeDelete<Event>($"events/{eventId}");
            return RedirectToAction(nameof(Index));
        }


    }
}
