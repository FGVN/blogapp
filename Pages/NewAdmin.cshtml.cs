using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using blogapp.Controllers;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Pages
{
    public class NewAdmin : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public JsonLogedService LogedService;
        public LoginController loginController;
        
        /// <summary>
        /// Configuring controllers and services
        /// </summary>
        public NewAdmin(ILogger<IndexModel> logger,
                    JsonLogedService logedservice)
        {
            _logger = logger;
            LogedService = logedservice;
            loginController = new LoginController(LogedService);
        }

        /// <summary>
        /// Gets value from form and passes it to the controller to add admin
        /// </summary>
        /// <returns>Redirect to Index if admin has been added, NewAdmin#Error if not</returns>
        public IActionResult OnPost()
        {
            string username = Request.Form["username"];

            if (loginController.AddAdmin(username))
                return Redirect("/Index");
            return Redirect("/NewAdmin#Error");
        }
    }
}
