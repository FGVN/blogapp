using blogapp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blogapp.Services
{
    public class JsonArticleService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonArticleService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment; 
        }
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "articles.json"); }
        }

        public IEnumerable<Article> GetArticles()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Article[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public void AddComment(Comment toAdd, Article article)
        {
            IEnumerable<Article> articles = GetArticles();

            var query = articles.First(x => x._title == article._title);

            var comments = query._comments.ToList();
            comments.Add(toAdd);
            query._comments.ToArray();

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Article>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    articles
                );
            }
        }

        public void AddReaction(Article toReact, Reaction reaction)
        {
            var articles = GetArticles();
            var query = articles.First(x => x == toReact);
            query._reactions.Add(reaction);

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Article>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    articles
                );
            }
        }

        public void SaveArticles(IEnumerable<Article> articles)
        {
            using var outputStream = File.OpenWrite(JsonFileName);
            JsonSerializer.Serialize<IEnumerable<Article>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                articles
            );
        }
    }
}
