using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using web.Models;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<HomeController> _logger;

    public HomeController(SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public IActionResult SignOutIfCookieIsEmpty()
    {
        // Get the loginData cookie
        var loginDataCookie = Request.Cookies["loginData"];

        if (string.IsNullOrEmpty(loginDataCookie))
        {
            // Set the loginData cookie to an empty string
            Response.Cookies.Append("loginData", "", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            });

            // Sign the user out
            _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }

        // Return an empty result
        return new EmptyResult();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Main()
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
        return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
