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

        public IActionResult AddArticle(Article toAdd)
        {
            if (toAdd._author == "" || toAdd._header == "" || toAdd._about == "" || toAdd._title == "" || toAdd._text == "")
            {
                return Redirect("/NewArticle");
            }
            if (ArticleService.AddArticle(toAdd))
            {
                return Redirect("/Index");
            }
            return Redirect("/NewArticle#Error");
        }

        public IActionResult EditArticle(Article toAdd)
        {
            if (toAdd._author == "" || toAdd._header == "" || toAdd._about == "" || toAdd._title == "" || toAdd._text == "")
            {
                return Redirect("/EditArticle#Error");
            }
            if (ArticleService.EditArticle(toAdd))
            {
                return Redirect("/Index");
            }
            return Redirect("/EditArticle#Error");
        }

        public IActionResult DeleteArticle(string titleToDelete)
        {
            if (ArticleService.DeleteArticle(titleToDelete))
            {
                return Redirect("/");
            }
            return Redirect("/" + titleToDelete);
        }
        

        public IEnumerable<Article> GetArticles()
        {
            return ArticleService.GetArticles();
        }
    }
}
