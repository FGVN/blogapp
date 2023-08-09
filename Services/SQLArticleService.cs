using blogapp.Data;
using blogapp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Differencing;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace blogapp.Services
{
    public class SQLArticleService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public AppDbContext _dbcontext { get; set; }
        public SQLArticleService(IWebHostEnvironment webHostEnvironment) 
        {
            WebHostEnvironment = webHostEnvironment;
            _dbcontext = new AppDbContext();
        } 

        public List<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();
            using (MySqlConnection connection = _dbcontext.Connect())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM articles";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Read the data
                        while (reader.Read())
                        {
                            articles.Add(new Article(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetDateTime(6),
                                reader.GetInt32(7)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            if (articles != null)
                return articles;
            return new List<Article>();
        }

        public bool AddArticle(Article toAdd)
        {

            string query = "INSERT INTO articles (header, about, title, text, author, post_date, viewcount)" +
                " VALUES ('" +
                toAdd._header+ "', '" +
                toAdd._about + "', '" +
                toAdd._title + "', '" +
                toAdd._text + "', '" +
                toAdd._author + "', '" +
                toAdd._postDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" +
                toAdd._viewcount + "')";

            //Defining cshtml to copy from and cshtml to copy to
            var sourceCSHTML = Path.Combine(WebHostEnvironment.ContentRootPath, "Pages", "testPage.cshtml");

            var destinationCSHTML = Path.Combine(WebHostEnvironment.ContentRootPath,
                "Pages", toAdd._title.Replace(" ", "-") + ".cshtml");


            //Changing articles id in cshtml to the one that new one has
            try
            {
                System.IO.File.Copy(sourceCSHTML, destinationCSHTML);

                var text = File.ReadAllText(destinationCSHTML);
                text = text.Replace("[0]", "[" + (GetArticles().ToArray().Length) + "]");
                File.WriteAllText(destinationCSHTML, text);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            return _dbcontext.RunQuery(query);
        }

        public bool EditArticle(Article toEdit)
        {
            string query = "UPDATE articles " +
                "SET header = '"+ toEdit._header + 
                "', about = '" + toEdit._about + 
                "', text = '" + toEdit._text + 
                "', author = '" + toEdit._author +
                "' WHERE title = '" + toEdit._title + "'";

            Console.WriteLine(query);
            return _dbcontext.RunQuery(query);
        }

        public bool DeleteArticle(string toDeleteTitle)
        {
            string query = "DELETE FROM articles " +
                "WHERE title = '" + toDeleteTitle + "'";
            File.Delete(Path.Combine(WebHostEnvironment.ContentRootPath,
                "Pages", toDeleteTitle.Replace(" ", "-") + ".cshtml"));
            return _dbcontext.RunQuery(query);
        }

        public void IncreaseView(string title)
        {
            string query = "UPDATE articles " +
                "SET viewcount = viewcount + 1 " +
                "WHERE title = '" + title + "'";
            _dbcontext.RunQuery(query);
        }
    }
}
