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

    /// <summary>
    /// Configuring controllers and services
    /// </summary>
    public Login(ILogger<IndexModel> logger,
                JsonLogedService logedservice)
    {
        _logger = logger;
        LogedService = logedservice;
        loginController = new LoginController(LogedService);
    }

    /// <summary>
    /// Getting login and password from form, checking if logincontroller login method returned not null user, adding cookies
    /// </summary>
    /// <returns>Redirect to Index if user has been loged in, Login#Error if not</returns>
    public IActionResult OnPost()
    {
        string login = Request.Form["login"];

        string password = Request.Form["password"];

        //Hashing password value
        password = HashController.HashString(password);

        Loged result = loginController.Login(login, password);

        if(result._login != null)
        {
            //Adding returned user data to cookie
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

