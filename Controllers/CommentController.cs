using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Controllers
{
    public class CommentController : Controller
    {
        JsonCommentService commentService;
        JsonLogedService logedService;
        public CommentController(JsonCommentService jsonCommentService, JsonLogedService jsonLogedService)
        {
            commentService = jsonCommentService;
            logedService = jsonLogedService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addComment(Article article, Loged commentator, Comment toAdd)
        {
            var users = logedService.GetLoged();

            if (users.FirstOrDefault(x => x._login == commentator._login)._password != commentator._password)
                return Redirect("/" + article._title.Replace(" ", "-") + "#CredentialError");

            commentService.AddComment(toAdd);

            return Redirect("/" + article._title.Replace(" ", "-"));
        }

        public IActionResult addReply(Article article, Loged commentator, Comment toAdd, Comment Reply)
        {
            var users = logedService.GetLoged();

            if (users.FirstOrDefault(x => x._login == commentator._login)._password != commentator._password)
                return Redirect("/" + article._title.Replace(" ", "-") + "#CredentialError");

            commentService.AddReply(toAdd, Reply);

            return Redirect("/" + article._title.Replace(" ", "-"));
        }
    }
}
