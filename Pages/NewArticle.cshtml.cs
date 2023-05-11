using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using blogapp.Controllers;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Pages
{
    public class NewArticle : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public JsonArticleService ArticleService;
        public ArticleController articleController;
        public NewArticle(ILogger<IndexModel> logger,
                    JsonArticleService articleservice)
        {
            _logger = logger;
            ArticleService = articleservice;
            articleController = new ArticleController(ArticleService);
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string header = Request.Form["header"];

            string about = Request.Form["about"];

            string title = Request.Form["title"];

            string text = Request.Form["text"];

            var username = Request.Cookies["username"];

                    //Response.Cookies.Append("username", username, cookieOptions);

                    //Response.Cookies.Append("login", login, cookieOptions);

                    //Response.Cookies.Append("password", password, cookieOptions);

            //Response.Cookies.Append("isAdmin", loginController.Login(login, password)._isAdmin.ToString(), cookieOptions);

            return articleController.Post(header, about, title, text, username);

        }
    }
}
