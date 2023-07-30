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
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "articleData.json"); }
        }

        public List<Article> GetArticles()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var empty = new List<Article>();
            var res = JsonSerializer.Deserialize<List<Article>>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if (res != null)
                return res;
            return empty;
        }

        public bool AddArticle(Article toAdd)
        {
            var articles = GetArticles();

            if (!articles.Select(x => x._title).Contains(toAdd._title))
            {
                if(articles.Count() != 0)
                    toAdd._id = articles.Last()._id + 1;

                articles.Add(toAdd);


                //var sourceCS = WebHostEnvironment.ContentRootPath + "\\Pages\\" + "testPage" + ".cshtml.cs";
                var sourceCSHTML = Path.Combine(WebHostEnvironment.ContentRootPath, "Pages", "testPage.cshtml");

                Console.WriteLine(WebHostEnvironment.ContentRootPath);


                //var destinationCS = WebHostEnvironment.ContentRootPath + "\\Pages\\" + toAdd._title.Replace(" ", "-") + ".chtml.cs";

                var destinationCSHTML = Path.Combine(WebHostEnvironment.ContentRootPath, "Pages" ,toAdd._title.Replace(" ", "-") + ".cshtml"); 



                try
                {
                    System.IO.File.Copy(sourceCSHTML, destinationCSHTML);

                    var text = File.ReadAllText(destinationCSHTML);
                    text = text.Replace("[0]", "["+(toAdd._id-1)+"]");
                    File.WriteAllText(destinationCSHTML, text);
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }


                File.WriteAllText(JsonFileName, string.Empty);

                using var outputStream = File.OpenWrite(JsonFileName);

                JsonSerializer.Serialize<IEnumerable<Article>>(
                     new Utf8JsonWriter(outputStream, new JsonWriterOptions
                     {
                         SkipValidation = true,
                         Indented = true
                     }),
                    articles
                );
                return true;
            }
            return false;
        }

        public bool EditArticle(Article toEdit)
        {
            var articles = GetArticles();

            int old_id = articles.First(x => x._title == toEdit._title)._id;
            int old_viewcount = articles.First(x => x._title == toEdit._title)._viewcount;
            DateTime old_date = articles.First(x => x._title == toEdit._title)._postDate;

            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))] = toEdit;

            //Putting some of the old values that has change by creating another instance of Article
            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))]._id = old_id;

            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))]._viewcount = old_viewcount;

            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))]._postDate = old_date;

            File.WriteAllText(JsonFileName, string.Empty);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Article>>(
                 new Utf8JsonWriter(outputStream, new JsonWriterOptions
                 {
                     SkipValidation = true,
                     Indented = true
                 }),
                articles
            );
            return true;
        }

        public void IncreaseView(int id)
        {
            var articles = GetArticles();

            articles.First(x => x._id == id)._viewcount++;

            File.WriteAllText(JsonFileName, string.Empty);

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

        public void AddComment(Comment toAdd, Article article)
        {
            /*IEnumerable<Article> articles = GetArticles();

            var query = articles.First(x => x._id == article._id);

            var comments = query.ToList();
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
            }*/
        }

        public void AddReaction(Article toReact, Reaction reaction)
        {
            /*var articles = GetArticles();
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
            }*/
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
