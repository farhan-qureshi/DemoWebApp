using DemoWebApp.Models;
using DemoWebApp.Providers;
using DemoWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInterestsService _interestsService;

        public HomeController(IInterestsService interestsService)
        {
            _interestsService = interestsService;
        }

        public IActionResult Interests()
        {
            var interestsForUser = _interestsService.GetAllInterestsForUser("user2");
            var model = new InterestsViewModel();
            model.Interests = interestsForUser.Result;
            return View(model);
        }

        [HttpPost]
        public JsonResult Subscribe(string title)
        {
            // make an API call to subscribe
            _interestsService.Subscribe("user2", new Interest() { Title = title });

            // send slack notifications (although my intention was to have it in a separate console application but I am limited by time)
            // Call the Api to get the content of the interest and send 

            var content = _interestsService.GetContentForInterest(title);
            NotificationProvider.SendSlackNotification("Demo Message", "user2", title, content.Result);

            return Json("");
        }

        [HttpPost]
        public JsonResult UnSubscribe(string title)
        {
            // make an API call to unsubscribe
            _interestsService.UnSubscribe("user2", new Interest() { Title = title });
            return Json("");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
