using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Controllers
{
    /// <summary>
    /// Controller that passes data to the service
    /// </summary>
    public class LoginController : ControllerBase
    {
        public SQLLogedService LogedService { get; }
        /// <summary>
        /// Configureing service
        /// </summary>
        public LoginController(SQLLogedService logedService) => LogedService = logedService;

        /// <summary>
        /// Adds new users data
        /// </summary>
        /// <param name="username">Username to add</param>
        /// <param name="login">Login to add</param>
        /// <param name="password">Password to add</param>
        /// <returns>Redirects to Signup if some field is empty,
        /// to Signup#Error if service has send false result,
        /// to Index if new users data has been added</returns>
        public IActionResult Post(
            [FromQuery] string username,
            [FromQuery] string login,
            [FromQuery] string password)
        {
            if(username == "" || login == "" || password == "")
                return Redirect("/Signup");
            if (LogedService.AddLoged(new Loged(username, login, password)))
                return Redirect("/Index");
            return Redirect("/Signup#Error");
        }

        /// <summary>
        /// Changes users isAdmin field value to true
        /// </summary>
        /// <param name="username">Username to give admin rights</param>
        /// <returns>Service`s respond if username is not empty, false if not</returns>
        public bool AddAdmin(string username)
        {
            if (username != "")
                return LogedService.AddAdmin(username);
            return false;
        }
        /// <summary>
        /// By login and password returns users data
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">Users password</param>
        /// <returns>User with null values if login or password is empty, if not - services respond</returns>
        public Loged Login(string login, string password)
        {
            if (login == "" || password == "")
                return new Loged(null, null, null);
            return LogedService.Login(login, password);
        }
    }
}
