using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using blogapp.Models;
using blogapp.Controllers;
using blogapp.Services;

namespace blogapp.Pages
{
    public class EditArticle : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public SQLArticleService ArticleService;
        public ArticleController articleController;

        public Article toEdit { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// Configuring controllers and services
        /// </summary>
        public EditArticle(ILogger<IndexModel> logger,
                    SQLArticleService articleservice)
        {
            _logger = logger;
            ArticleService = articleservice;
            articleController = new ArticleController(ArticleService);
        }
        /// <summary>
        /// Checking cookies values on load and adding value to toEdit Article field
        /// </summary>
        public void OnGet()
        {

            if(Request.Cookies["idToEdit"] != null)
                toEdit = articleController.GetArticles().First(x => x._id == Convert.ToInt32(Request.Cookies["idToEdit"]));
            if (Request.Cookies["tempTitle"] != null)
            {
                Console.WriteLine("Title" + Request.Cookies["tempTitle"]);
                toEdit = articleController.GetArticles().First(x => x._title == Request.Cookies["tempTitle"]);
                articleController.DeleteArticle(Request.Cookies["tempTitle"]);
            }
        }

        /// <summary>
        /// Conservates current article values, setting up a cookie with its name and adding image
        /// </summary>
        /// <returns>Redirects on EditArticle page</returns>
        public async Task<IActionResult> OnPostEdit()
        {
            //Saving current values into new Article 
            string header = Request.Form["header"];

            string about = Request.Form["about"];

            string title = Request.Form["title"];

            string text = Request.Form["text"];

            var username = Request.Cookies["username"];

            articleController.AddArticle(new Article(header, about, title, text, username, DateTime.Now));

            //Setting a cookie with an articles name
            var cookieOptions = new CookieOptions();

            cookieOptions.Path = "/";

            Response.Cookies.Append("tempTitle", Request.Form["title"], cookieOptions);

            //Adding image
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
                return new RedirectToPageResult("/EditArticle");
            }

            _logger.LogInformation($"No image file provided.");
            return new RedirectToPageResult("/EditArticle");
        }

        /// <summary>
        /// Gets current values from form and sends it to controller
        /// </summary>
        /// <returns>Controllers EditArticle result</returns>
        public IActionResult OnPost()
        {

            string header = Request.Form["header"];

            string about = Request.Form["about"];

            string title = Request.Form["title"];

            title = title.Split(',')[0];

            string text = Request.Form["text"];

            var username = Request.Cookies["username"];

            //???Datetime
            return articleController.EditArticle(new Article(header, about, title, text, username, DateTime.Now));
        }
    }
}
