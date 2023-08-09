using blogapp.Data;
using blogapp.Models;
using MySql.Data.MySqlClient;

namespace blogapp.Services
{
    public class SQLCommentService
    {
        public AppDbContext _dbcontext { get; set; }
        public SQLCommentService(IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = new AppDbContext();
        }

        public List<Comment> GetReplies(int comment_id)
        {
            List<Comment> replies = new List<Comment>();
            using (MySqlConnection connection = _dbcontext.Connect())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM comment_replies WHERE comment_id = " + comment_id;
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Read the data
                        while (reader.Read())
                        {
                            replies.Add(new Comment(
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                new List<Comment>(),
                                reader.GetDateTime(4)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            if (replies != null)
                return replies;
            return new List<Comment>();
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            using (MySqlConnection connection = _dbcontext.Connect())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM comments";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Read the data
                        while (reader.Read())
                        {
                            comments.Add(new Comment(
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                GetReplies(reader.GetInt32(0)),
                                reader.GetDateTime(4)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            if (comments != null)
                return comments;
            return new List<Comment>();
        }

        public bool AddComment(Comment toAdd)
        {
            string query = "INSERT INTO comments (article_id, username, text, post_date)" +
                " VALUES (" +
                toAdd._article_id + ", '" +
                toAdd._username + "', '" +
                toAdd._text + "', '" +
                toAdd._postDate.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            return _dbcontext.RunQuery(query);
        }

        public bool AddReply(Comment toAdd, Comment Reply)
        {
            var comments = GetComments();
            string query = "INSERT INTO comment_replies (comment_id, username, text, post_date)" +
                " VALUES (" +
                (comments.IndexOf(comments.FirstOrDefault(x => x._postDate == toAdd._postDate))+1) + ", '" +
                Reply._username + "', '" +
                Reply._text + "', '" +
                Reply._postDate.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            Console.WriteLine(query);
            return _dbcontext.RunQuery(query);
        }
    }
}
