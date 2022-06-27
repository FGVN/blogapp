using blogapp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blogapp.Services
{
    public class JsonCommentService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonCommentService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "comments.json"); }
        }
        public IEnumerable<Comment> GetArticles()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Comment[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
