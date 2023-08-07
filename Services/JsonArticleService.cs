using blogapp.Models;
using System.Text.Json;

namespace blogapp.Services
{
    /// <summary>
    /// Service that operates with json with an articles data
    /// </summary>
    public class JsonArticleService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonArticleService(IWebHostEnvironment webHostEnvironment) => WebHostEnvironment = webHostEnvironment; 
        /// <summary>
        /// Setting up a path to a json file with articles data
        /// </summary>
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "articleData.json"); 

        /// <summary>
        /// Gets list of all articles from a json file
        /// </summary>
        /// <returns>List of all articles inside a json or new list</returns>
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

        /// <summary>
        /// Adds an article to json file
        /// </summary>
        /// <param name="toAdd">Article to add to json</param>
        /// <returns>True if article has been added to json, false if not</returns>
        public bool AddArticle(Article toAdd)
        {
            var articles = GetArticles();

            if (!articles.Select(x => x._title).Contains(toAdd._title))
            {
                if(articles.Count() != 0)
                    toAdd._id = articles.Last()._id + 1;

                articles.Add(toAdd);

                //Defining cshtml to copy from and cshtml to copy to
                var sourceCSHTML = Path.Combine(WebHostEnvironment.ContentRootPath, "Pages", "testPage.cshtml");

                var destinationCSHTML = Path.Combine(WebHostEnvironment.ContentRootPath, 
                    "Pages" ,toAdd._title.Replace(" ", "-") + ".cshtml"); 


                //Changing articles id in cshtml to the one that new one has
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

                //Saving articles data into json file
                SaveArticles(articles);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes values for a certain article from the list
        /// </summary>
        /// <param name="toEdit">Article with new values</param>
        public bool EditArticle(Article toEdit)
        {
            //Getting values that should not change
            var articles = GetArticles();

            int old_id = articles.First(x => x._title == toEdit._title)._id;
            int old_viewcount = articles.First(x => x._title == toEdit._title)._viewcount;
            DateTime old_date = articles.First(x => x._title == toEdit._title)._postDate;

            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))] = toEdit;

            //Putting some of the old values that has change by creating another instance of Article
            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))]._id = old_id;

            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))]._viewcount = old_viewcount;

            articles[articles.IndexOf(articles.First(x => x._title == toEdit._title))]._postDate = old_date;

            //Saving data to json file
            SaveArticles( articles );
            return true;
        }

        /// <summary>
        /// Deletes article from a list by its title
        /// </summary>
        /// <param name="toDeleteTitle">Title of article that should be deleted</param>
        /// <returns>True if an article with given title has been deleted, false if not</returns>
        public bool DeleteArticle(string toDeleteTitle)
        {
            var articles = GetArticles();

            if(articles.Where(x => x._title == toDeleteTitle).Any())
            {
                articles.Remove(articles.First(x => x._title == toDeleteTitle));

                string destinationCSHTML = Path.Combine(WebHostEnvironment.ContentRootPath, "Pages", toDeleteTitle + ".cshtml");

                if(File.Exists(destinationCSHTML))
                    File.Delete(destinationCSHTML);

                SaveArticles(articles);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Increases viewcount of an article
        /// </summary>
        /// <param name="id">Id of an article to update viewcount</param>
        public void IncreaseView(int id)
        {
            var articles = GetArticles();

            articles.First(x => x._id == id)._viewcount++;

            SaveArticles(articles);
        }

        /// <summary>
        /// Puts IEnumerable<Article> into json
        /// </summary>
        /// <param name="articles">Updated list of articles</param>
        public void SaveArticles(IEnumerable<Article> articles)
        {

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
    }
}
