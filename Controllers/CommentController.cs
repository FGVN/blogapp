using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Controllers
{
    /// <summary>
    /// Controller that passes data to the service
    /// </summary>
    public class CommentController : Controller
    {
        SQLCommentService commentService;
        /// <summary>
        /// Configuring service
        /// </summary>
        /// <param name="jsonCommentService"></param>
        public CommentController(SQLCommentService CommentService) => commentService = CommentService;

        /// <summary>
        /// Passes data about comment to add to the controller
        /// </summary>
        /// <param name="article">Article to redirect to after comment been added</param>
        /// <param name="toAdd">Comment to add</param>
        /// <returns>Redirect on article article page</returns>
        public IActionResult addComment(Article article, Comment toAdd)
        {
            commentService.AddComment(toAdd);

            return Redirect("/" + article._title.Replace(" ", "-"));
        }

        /// <summary>
        /// Adds reply to some comments reply list
        /// </summary>
        /// <param name="article">Article to redirect after</param>
        /// <param name="toAdd">Comment to add reply to</param>
        /// <param name="Reply">Reply to add to the comments reply list</param>
        /// <returns>Redirect to the provided article page</returns>
        public IActionResult addReply(Article article, Comment toAdd, Comment Reply)
        {
            commentService.AddReply(toAdd, Reply);

            return Redirect("/" + article._title.Replace(" ", "-"));
        }
    }
}
