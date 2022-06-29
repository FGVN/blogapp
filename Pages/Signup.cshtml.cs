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
        public Loged toAdd = new Loged(null,null,null);

        public string username {get; set;}
        public string login {get; set;}
        public string password {get; set;}

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

        [HttpPost]
        public void OnPost()
        {
            string username = Request.Form["username"];

            string login = Request.Form["login"];

            string password = Request.Form["password"];
            
            loginController.Post(username, login, password);
        }


    }
}



