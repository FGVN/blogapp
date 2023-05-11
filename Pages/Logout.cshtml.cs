using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace blogapp.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            var cookieOptions = new CookieOptions();

            cookieOptions.Path = "/";

            Response.Cookies.Delete("username");

            Response.Cookies.Delete("login");

            Response.Cookies.Delete("password");

            Response.Cookies.Delete("isAdmin");

            return Redirect("/");
        }
    }
}
