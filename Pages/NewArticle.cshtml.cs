using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public Article tempArticle;
        public NewArticle(ILogger<IndexModel> logger,
                    JsonArticleService articleservice)
        {
            _logger = logger;
            ArticleService = articleservice;
            articleController = new ArticleController(ArticleService);
        }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public void OnGet()
        {
            //Check for temp article id in cookies and put values if its not empty and delete right after
            if (Request.Cookies["tempTitle"] != null)
            {
                var articles = articleController.GetArticles();
                tempArticle = articles.First(x => x._title == Request.Cookies["tempTitle"]);
                articleController.DeleteArticle(tempArticle._title);
            }
            else
            {
                tempArticle = new Article("", "", "", "", "", DateTime.Now);
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            //Create temp article with current values, set cookie with temp article id
            string header = Request.Form["header"];

            string about = Request.Form["about"];

            string title = Request.Form["title"];

            string text = Request.Form["text"];

            var username = Request.Cookies["username"];

            articleController.AddArticle(new Article(header, about, title, text, username, DateTime.Now));

            var cookieOptions = new CookieOptions();

            cookieOptions.Path = "/";

            Response.Cookies.Append("tempTitle", Request.Form["title"], cookieOptions);

            //Image processing
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "pics");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                _logger.LogInformation($"Image '{fileName}' uploaded successfully.");

                return new RedirectToPageResult("/NewArticle");
            }

            _logger.LogInformation($"No image file provided.");
            return new RedirectToPageResult("/NewArticle");
        }

        public IActionResult OnPost()
        {
            string header = Request.Form["header"];

            string about = Request.Form["about"];

            string title = Request.Form["title"];

            string text = Request.Form["text"];

            var username = Request.Cookies["username"];

            return articleController.AddArticle(new Article(header, about, title, text, username, DateTime.Now));

        }
    }
}

