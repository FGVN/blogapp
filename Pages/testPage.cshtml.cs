using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using blogapp.Controllers;
using blogapp.Services;
using blogapp.Models;
using System.Net;

namespace blogapp.Pages
{
    public class testPageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public JsonArticleService ArticleService;
        public JsonCommentService CommentService;
        public ArticleController articleController;
        public CommentController commentController;
        public ReactionController reactionController;

        public List<Comment> comments;
        public Article[] articles;
        public int[] reactions;
        public testPageModel(ILogger<IndexModel> logger,
                    JsonArticleService articleservice,
                    JsonLogedService logedservice,
                    JsonCommentService commentservice,
                    JsonReactionService reactionservice)
        {
            _logger = logger;
            ArticleService = articleservice;
            CommentService = commentservice;
            articleController = new ArticleController(ArticleService);
            commentController = new CommentController(commentservice, logedservice);
            reactionController = new ReactionController(reactionservice);
        }
        public void OnGet()
        {
            articles = ArticleService.GetArticles().ToArray();
            comments = CommentService.GetComments().ToList();


            //WORKING!
            //
            string pageName = RouteData.Values["page"].ToString();
            Console.WriteLine(pageName);
            reactions = reactionController.Display(
                articles.FirstOrDefault(x => x._title == pageName.Replace("/", string.Empty))._id
            );
            //Dont know how to get article id on Get
            //Console.WriteLine("AId:" + Request.Cookies["art_id"]);
            //if (Request.Cookies["art_id"] != null)
            //{
            //    reactions = reactionController.Display(Convert.ToInt32(Request.Cookies["art_id"]));
            //    Response.Cookies.Delete("art_id");
            //}
            //else
            //    reactions = reactionController.Display(0);
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

        public IActionResult OnPostUpdate()
        {
            //Absorb data from forms in and put them into controller
            return reactionController.React(new Reaction(
                Convert.ToInt32(Request.Form["id"]),
                Request.Cookies["username"],
                Convert.ToInt32(Request.Form["SubmitButton"])));
        }
    }
}
