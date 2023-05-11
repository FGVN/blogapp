using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController(JsonLogedService logedService)
        {
            LogedService = logedService;
        }

        public JsonLogedService LogedService { get; }

        [Route("Sign")]
        [HttpPost]

        public IActionResult Post(
            [FromQuery] string username,
            [FromQuery] string login,
            [FromQuery] string password)
        {
            if(username == "" || login == "" || password == "")
            {
                return Redirect("/Signup");
            }
            if (LogedService.AddLoged(new Loged(username, login, password)))
            {
                return Redirect("/Index");
            }
            return Redirect("/Signup#Error");
        }


        public bool AddAdmin(string username)
        {
            if (username != "")
                return LogedService.AddAdmin(username);
            return false;
        }
        public Loged Login(string login, string password)
        {
            if (login == "" || password == "")
                return new Loged(null, null, null);
            return LogedService.Login(login, password);
        }


        [HttpGet]
        public IEnumerable<Loged> GetLoged()
        {
            return LogedService.GetLoged();
        }

        /*public ActionResult Get(
            [FromQuery] string username,
            [FromQuery] string login,
            [FromQuery] string password)
        {
            LogedService.AddLoged(new Loged(username, login, password));
            return Ok();
        }*/
    }
}
