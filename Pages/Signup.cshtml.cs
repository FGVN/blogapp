using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using blogapp.Controllers;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Pages
{
    public class Signup : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public JsonLogedService LogedService;
        public LoginController loginController;

        public Signup(ILogger<IndexModel> logger,
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
            string username = Request.Form["username"];

            string login = Request.Form["login"];

            string password = Request.Form["password"];

            var cookieOptions = new CookieOptions();

            cookieOptions.Path = "/";

            Response.Cookies.Append("username", username, cookieOptions);

            Response.Cookies.Append("login", login, cookieOptions);

            Response.Cookies.Append("password", password, cookieOptions);

            Response.Cookies.Append("isAdmin", loginController.Login(login, password)._isAdmin.ToString(), cookieOptions);

            return loginController.Post(username, login, password);
            
        }


    }
}



