using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using blogapp.Controllers;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Pages
{
    public class testPageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public JsonArticleService ArticleService;
        public JsonCommentService CommentService;
        public JsonLogedService LogedService;

        public ArticleController articleController;
        public CommentController commentController;
        public ReactionController reactionController;

        public List<Comment> comments;
        public Article[] articles;
        public int[] reactions;

        /// <summary>
        /// Configuring controllers and services
        /// </summary>
        public testPageModel(ILogger<IndexModel> logger,
                    JsonArticleService articleservice,
                    JsonLogedService logedservice,
                    JsonCommentService commentservice,
                    JsonReactionService reactionservice)
        {
            _logger = logger;
            ArticleService = articleservice;
            CommentService = commentservice;
            LogedService = logedservice;
            articleController = new ArticleController(articleservice);
            commentController = new CommentController(commentservice);
            reactionController = new ReactionController(reactionservice);
        }
        /// <summary>
        /// Getting list of all articles, comments and reactions to display
        /// </summary>
        public void OnGet()
        {
            articles = ArticleService.GetArticles().ToArray();
            comments = CommentService.GetComments().ToList();

            string pageName = RouteData.Values["page"].ToString();

            reactions = reactionController.Display(
                articles.FirstOrDefault(x => x._title == pageName.Replace("/", string.Empty).Replace("-", " "))._id
            );
        }
         
        /// <summary>
        /// Updating viewcount for an article
        /// </summary>
        /// <param name="id">Id of an article to update viewcount</param>
        public Article updateViewcount(int id)
        {
            ArticleService.IncreaseView(id);
            return articles[0];
        }

        /// <summary>
        /// Adding a comment
        /// </summary>
        /// <returns>Redirect to Login if user do not exist, controller result if exists</returns>
        public IActionResult OnPost()
        {
            if (!IsUserExists())
                return Redirect("/Login");

            Comment toAdd = new Comment(Convert.ToInt32(Request.Form["id"]), Request.Cookies["username"], Request.Form["text"]);

            return commentController.addComment(
                ArticleService.GetArticles().First(x => x._id == Convert.ToInt32(Request.Form["id"])),
                toAdd);
        }

        /// <summary>
        /// Adding a reply to a comment
        /// </summary>
        /// <returns>Redirect to Login if user do not exists controller result if exists</returns>
        public IActionResult OnPostEdit()
        {
            var username = Request.Cookies["username"];
            var login = Request.Cookies["login"];
            var password = Request.Cookies["password"];

            if (!IsUserExists())
                return Redirect("/Login");

            var toAddDate = Convert.ToDateTime(Request.Form["postDate"]);

            Comment toAdd = CommentService.GetComments().ToList().
                Where(x => x._username == Request.Form["username"]).
                First(x => 
                x._postDate.Date == toAddDate.Date && 
                x._postDate.Hour == toAddDate.Hour &&
                x._postDate.Minute == toAddDate.Minute &&
                x._postDate.Second == toAddDate.Second);

            Comment reply = new Comment(Convert.ToInt32(Request.Form["reply_id"]), username, Request.Form["reply_text"]);

            Console.WriteLine(Request.Form["reply_id"]);

            return commentController.addReply(
                ArticleService.GetArticles().First(x => x._id == Convert.ToInt32(Request.Form["id"])),
                toAdd, reply);
        }

        /// <summary>
        /// Adding a reaction 
        /// </summary>
        /// <returns>Redirect to Login if user do not exist, controller result if exists</returns>
        public IActionResult OnPostUpdate()
        {
            if (!IsUserExists())
                return Redirect("/Login");
            return reactionController.React(new Reaction(
                Convert.ToInt32(Request.Form["id"]),
                Request.Cookies["username"],
                Convert.ToInt32(Request.Form["SubmitButton"])));
        }
        
        /// <summary>
        /// Deletes an article
        /// </summary>
        public IActionResult OnPostDelete()
        {
            return articleController.DeleteArticle(
                RouteData.Values["page"].ToString().Replace("/", string.Empty).Replace("-", " ")
            );
        }

        /// <summary>
        /// Checks cookies and tells if users exists
        /// </summary>
        /// <returns>True if user exists, false if not</returns>
        public bool IsUserExists() => LogedService.GetLoged().Where(x =>
            x._username == Request.Cookies["username"]
            && x._login == Request.Cookies["login"]
            && x._password == Request.Cookies["password"]).Any();
    }
}
