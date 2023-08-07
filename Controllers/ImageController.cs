using Microsoft.AspNetCore.Mvc;

namespace blogapp.Controllers
{
    /// <summary>
    /// Controller that is responsible for image uploading process
    /// </summary>
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Configuring webhostenvironment
        /// </summary>
        public ImageController(IWebHostEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;

        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            // Assuming only one file is being uploaded
            var file = Request.Form.Files[0]; 

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "data", "pics");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
