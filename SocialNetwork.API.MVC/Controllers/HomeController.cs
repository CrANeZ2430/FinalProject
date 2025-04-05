using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models;
using System.Diagnostics;

namespace SocialNetwork.API.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditUserProfile()
        {
            var model = new UserProfileFormViewModel(
                "example", 
                "example");

            return View(model);
        }

        public IActionResult ValidationError()
        {
            var errorDetails = HttpContext.Items["ErrorDetails"] as object;
            if (errorDetails != null)
            {
                // Pass the error details to the view
                return View(errorDetails);
            }

            return View("ErrorGeneric"); // Fallback error view if no specific details
        }
        public IActionResult AccessDenied()
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
