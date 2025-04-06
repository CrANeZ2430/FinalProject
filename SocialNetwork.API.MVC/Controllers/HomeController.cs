using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models.ViewModels;
using System.Diagnostics;

namespace SocialNetwork.API.MVC.Controllers;

public class HomeController(
    ILogger<HomeController> logger) 
    : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult EditUserProfileForm()
    {
        var model = new UserProfileFormViewModel(
            "example", 
            "example");

        return View(model);
    }

    [Authorize]
    public IActionResult AddPostForm()
    {
        var model = new AddPostFormViewModel(
            "example",
            "example");

        return View(model);
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
