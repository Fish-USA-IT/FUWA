using Microsoft.AspNetCore.Mvc;

namespace FishUsaWebApp.Controllers;

public abstract class AppController : Controller
{
    protected bool IsLoggedIn()
    {
        return !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeId"));
    }

    protected IActionResult RedirectToLogin()
    {
        return RedirectToAction("Login", "Auth");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToLogin();
    }
}