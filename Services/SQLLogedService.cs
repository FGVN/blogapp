using MySql.Data.MySqlClient;
using blogapp.Data;
using blogapp.Models;

namespace blogapp.Services
{
    public class SQLLogedService
    {
        public AppDbContext _dbcontext { get; set; }
        public SQLLogedService() => _dbcontext = new AppDbContext();

        public IEnumerable<Loged> GetLoged()
        {
            List<Loged> loged = new List<Loged>();
            using (MySqlConnection connection = _dbcontext.Connect())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM loged";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Read the data
                        while (reader.Read())
                        {
                            loged.Add(new Loged(
                                reader.GetString(1), 
                                reader.GetString(2), 
                                reader.GetString(3),
                                reader.GetBoolean(0)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            if(loged != null)
                return loged;
            return new List<Loged>();
        }

        public Loged Login(string login, string password)
        {
            var users = GetLoged();
            if (users.FirstOrDefault(x => x._login == login) != null &&
                users.FirstOrDefault(x => x._login == login) == users.FirstOrDefault(x => x._password == password))
            {
                return users.First(x => x._login == login);
            }
            return new Loged(null, null, null);
        }

        public bool AddAdmin(string username)
        {
            string query = "UPDATE loged SET _isAdmin = true WHERE _username = "+username;
            return _dbcontext.RunQuery(query);
        }

        public bool AddLoged(Loged toAdd)
        {
            string query = "INSERT INTO loged (_username, _login, _password, _isAdmin)" +
                " VALUES ('" +
                toAdd._username + "', '" +
                toAdd._login + "', '" +
                toAdd._password + "', " +
                (toAdd._isAdmin ? 1 : 0) + ")";
            Console.WriteLine(query);
            return _dbcontext.RunQuery(query);
        }
    }
}
