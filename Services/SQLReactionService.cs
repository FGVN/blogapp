using blogapp.Data;
using blogapp.Models;
using MySql.Data.MySqlClient;

namespace blogapp.Services
{
    public class SQLReactionService
    {
        public AppDbContext _dbcontext { get; set; }
        public SQLReactionService(IWebHostEnvironment webHostEnvironment) => _dbcontext = new AppDbContext();

        public List<Reaction> GetReactions()
        {
            List<Reaction> reactions = new List<Reaction>();
            using (MySqlConnection connection = _dbcontext.Connect())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM reactions";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Read the data
                        while (reader.Read())
                        {
                            reactions.Add(new Reaction(
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetInt32(3)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            if (reactions != null)
                return reactions;
            return new List<Reaction>();
        }

        public bool AddReaction(Reaction toAdd)
        {
            string query = "INSERT INTO reactions (article_id, username, reaction)" +
                " VALUES (" +
                toAdd._article_id + ", '" +
                toAdd._username + "', " +
                toAdd._reaction + ")";
            return _dbcontext.RunQuery(query);
        }

        public bool RemoveReaction(Reaction toRemove) 
        {
            string query = "DELETE FROM reactions " +
                "WHERE article_id = " + toRemove._article_id +
                " AND username = '" + toRemove._username + 
                "' AND reaction = " + toRemove._reaction;
            return _dbcontext.RunQuery(query);
        }
    }
}
