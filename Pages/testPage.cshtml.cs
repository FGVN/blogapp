using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
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
        public ArticleController articleController;
        public CommentController commentController;

        public List<Comment> comments;
        public Article[] articles;
        public testPageModel(ILogger<IndexModel> logger,
                    JsonArticleService articleservice,
                    JsonLogedService logedservice,
                    JsonCommentService commentservice)
        {
            _logger = logger;
            ArticleService = articleservice;
            CommentService = commentservice;
            articleController = new ArticleController(ArticleService);
            commentController = new CommentController(commentservice, logedservice);
        }
        public void OnGet()
        {
            articles = ArticleService.GetArticles().ToArray();
            comments = CommentService.GetComments().ToList();
        }

        public Article updateViewcount(int id)
        {
            ArticleService.IncreaseView(id);
            return articles[0];
        }

        public IActionResult OnPost()
        {
            var username = Request.Cookies["username"];
            var login = Request.Cookies["login"];
            var password = Request.Cookies["password"];

            Comment toAdd = new Comment(Convert.ToInt32(Request.Form["id"]), username, Request.Form["text"]);

            Console.WriteLine( Request.Form["id"]);

            return commentController.addComment(
                ArticleService.GetArticles().First(x => x._id == Convert.ToInt32(Request.Form["id"])),
                new Loged(username, login, password),
                toAdd);
        }

        public IActionResult OnPostEdit()
        {
            Console.WriteLine(Request.Form["username"] + "  " + Request.Form["id"]);
            var username = Request.Cookies["username"];
            var login = Request.Cookies["login"];
            var password = Request.Cookies["password"];
            var toAddDate = Convert.ToDateTime(Request.Form["postDate"]);


            //Comment toAdd = new Comment( 0, "", "");
            foreach(var i in CommentService.GetComments().ToList().
                Where(x => x._username == Request.Form["username"]))
            {
                Console.WriteLine(i._postDate + "/" + toAddDate + "res:" + DateTime.Compare(i._postDate, toAddDate));
                
            }

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
                new Loged(username, login, password),
                toAdd, reply);
        }
    }
}
