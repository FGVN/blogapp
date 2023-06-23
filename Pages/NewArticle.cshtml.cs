using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
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

                    //Response.Cookies.Append("username", username, cookieOptions);

                    //Response.Cookies.Append("login", login, cookieOptions);

                    //Response.Cookies.Append("password", password, cookieOptions);

            //Response.Cookies.Append("isAdmin", loginController.Login(login, password)._isAdmin.ToString(), cookieOptions);

            return articleController.Post(header, about, title, text, username);

        }
    }
}

