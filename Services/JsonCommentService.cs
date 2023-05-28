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

        public List<Comment> GetComments()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            
            var json = JsonSerializer.Deserialize<List<Comment>>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if(json != null)
                return json;
            return new List<Comment>();
        }
        public void AddComment(Comment toAdd)
        {
            var comments = GetComments().ToList();

            comments.Add(toAdd);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Comment>>(
                 new Utf8JsonWriter(outputStream, new JsonWriterOptions
                 {
                     SkipValidation = true,
                     Indented = true
                 }),
                comments
            );
        }

        public void AddReply(Comment toAdd, Comment Reply)
        {
            var comments = GetComments().ToList();

            comments.Where(x => x._username == toAdd._username).First(x => x._postDate == toAdd._postDate)._replies.Add(Reply);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Comment>>(
                 new Utf8JsonWriter(outputStream, new JsonWriterOptions
                 {
                     SkipValidation = true,
                     Indented = true
                 }),
                comments
            );
        }
    }
}
