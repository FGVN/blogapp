using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using blogapp.Controllers;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Pages;

public class Login : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public JsonLogedService LogedService;
    public LoginController loginController;

    public Login(ILogger<IndexModel> logger,
                JsonLogedService logedservice)
    {
        _logger = logger;
        LogedService = logedservice;
        loginController = new LoginController(LogedService);
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        string login = Request.Form["login"];

        string password = Request.Form["password"];

        password = HashController.HashString(password);

        Loged result = loginController.Login(login, password);

        if(result._login != null)
        {

            var cookieOptions = new CookieOptions();

            cookieOptions.Path = "/";

            Response.Cookies.Append("username", result._username, cookieOptions);

            Response.Cookies.Append("login", result._login, cookieOptions);

            Response.Cookies.Append("password", result._password, cookieOptions);

            Response.Cookies.Append("isAdmin", loginController.Login(login, password)._isAdmin.ToString(), cookieOptions);

            return Redirect("/Index");
        }


        return Redirect("/Login#Error");

    }
}

