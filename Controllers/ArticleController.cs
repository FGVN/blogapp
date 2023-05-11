using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Controllers
{
    public class ArticleController : ControllerBase
    {

        public ArticleController(JsonArticleService articleService)
        {
            ArticleService = articleService;
        }

        public JsonArticleService ArticleService { get; set; }

        public IActionResult Post(
            [FromQuery] string header, 
            [FromQuery] string about, 
            [FromQuery] string title, 
            [FromQuery] string text, 
            [FromQuery] string username)
        {
            if (username == "" || header == "" || about == "" || title == "" || text == "")
            {
                return Redirect("/NewArticle");
            }
            if (ArticleService.AddArticle(new Article(header, about, title, text, username, DateTime.Now)))
            {
                return Redirect("/Index");
            }
            return Redirect("/NewArticle#Error");
        }

        public IEnumerable<Article> GetArticles()
        {
            return ArticleService.GetArticles();
        }
    }
}
