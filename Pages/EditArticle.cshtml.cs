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

        public JsonArticleService ArticleService;
        public ArticleController articleController;

        public Article toEdit { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public EditArticle(ILogger<IndexModel> logger,
                    JsonArticleService articleservice)
        {
            _logger = logger;
            ArticleService = articleservice;
            articleController = new ArticleController(ArticleService);
        }
        public void OnGet()
        {
            toEdit = articleController.GetArticles().First(x => x._title == Request.Cookies["titleToEdit"]);
        }

        public async Task<IActionResult> OnPostEdit()
        {
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
                //Response.Cookies.Append("tmpImg", fileName, new CookieOptions());
                return new RedirectToPageResult("/EditArticle");
            }

            _logger.LogInformation($"No image file provided.");
            return new RedirectToPageResult("/EditArticle");
        }

        public IActionResult OnPost()
        {

            string header = Request.Form["header"];

            string about = Request.Form["about"];

            string title = Request.Form["title"];

            string text = Request.Form["text"];

            var username = Request.Cookies["username"];

            //???Datetime
            return articleController.EditArticle(new Article(header, about, title, text, username, DateTime.Now));
        }
    }
}
