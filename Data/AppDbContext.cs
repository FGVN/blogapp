using Microsoft.EntityFrameworkCore;
using blogapp.Models;
using MySql.Data.MySqlClient;

namespace blogapp.Data
{
    public class AppDbContext : DbContext
    {
        private static string _connectionString;
        public AppDbContext(string connectionString) => _connectionString = connectionString;
        public AppDbContext() { }

        public DbSet<Article> workshop { get; set; }

        public MySqlConnection Connect() => new MySqlConnection(_connectionString);


        public bool RunQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);

                    // Execute the query
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Update successful.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No rows updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return false;
        }
    }
}
