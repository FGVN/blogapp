using blogapp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blogapp.Services
{
    public class JsonReactionService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonReactionService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "reactions.json"); }
        }
        public IEnumerable<Reaction> GetReactions()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Reaction[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
