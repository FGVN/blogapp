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

        [HttpGet]
        public IEnumerable<Loged> Get()
        {
            return LogedService.GetLoged();
        }

        [Route("getloged")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery] string username,
            [FromQuery] string login,
            [FromQuery] string password)
        {
            LogedService.AddLoged(new Loged(username, login, password));
            return Ok();
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
