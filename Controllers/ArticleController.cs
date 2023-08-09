using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Controllers
{
    /// <summary>
    /// Controller that operates with articles and passes data to the service
    /// </summary>
    public class ArticleController : ControllerBase
    {
        public SQLArticleService ArticleService { get; set; }
        /// <summary>
        /// Setting up service
        /// </summary>
        /// <param name="articleService"></param>
        public ArticleController(SQLArticleService articleService) => ArticleService = articleService;

        /// <summary>
        /// Gets some article, checks if it is not empty and passes data to servcie
        /// </summary>
        /// <param name="toAdd">Article to add</param>
        /// <returns>
        /// Redirects on NewArticle page if some field is empty, 
        /// NewArticle#Error if service sends false result 
        /// and to Index if article has been added
        /// </returns>
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

        /// <summary>
        /// Checks values of an article to be edited and passes to the service
        /// </summary>
        /// <param name="toAdd">Article to edit</param>
        /// <returns>
        /// Redirects to EditArticle#Error if some field is empty or service sends false result,
        /// to Index if article has been successfuly edited
        /// </returns>
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

        /// <summary>
        /// Deletes some article by its title
        /// </summary>
        /// <param name="titleToDelete">Title of the article to delete</param>
        /// <returns>
        /// To index if article has been deleted,
        /// to page with title given if service sends false result
        /// </returns>
        public IActionResult DeleteArticle(string titleToDelete)
        {
            if (ArticleService.DeleteArticle(titleToDelete))
            {
                return Redirect("/");
            }
            return Redirect("/" + titleToDelete);
        }
        
        /// <summary>
        /// Gets all articles from service
        /// </summary>
        /// <returns>List of all articles returned by service</returns>
        public IEnumerable<Article> GetArticles() => ArticleService.GetArticles();
    }
}
