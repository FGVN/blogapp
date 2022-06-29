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

        public ActionResult Post(
            [FromQuery] string username,
            [FromQuery] string login,
            [FromQuery] string password)
        {
            LogedService.AddLoged(new Loged(username, login, password));

            return Ok();
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
