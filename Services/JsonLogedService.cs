using blogapp.Models;
using System.Text.Json;

namespace blogapp.Services
{
    /// <summary>
    /// Service that operates with loged json data
    /// </summary>
    public class JsonLogedService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonLogedService(IWebHostEnvironment webHostEnvironment) => WebHostEnvironment = webHostEnvironment;
        /// <summary>
        /// Configuring path to json file
        /// </summary>
        private string JsonFileName =>  Path.Combine(WebHostEnvironment.WebRootPath, "data", "loged.json"); 
        /// <summary>
        /// Gets list of users from a json file
        /// </summary>
        /// <returns>List of all users or a new lst</returns>
        public List<Loged> GetLoged()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var empty = new List<Loged>();
            var res = JsonSerializer.Deserialize<List<Loged>>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if(res != null)
                return res;
            return empty;
        }
        /// <summary>
        /// Checks if user exists with such a parameters
        /// </summary>
        /// <param name="login">Login of a user to find</param>
        /// <param name="password">Password of a user to find</param>
        /// <returns>Loged object that has given parameters or with null parameters</returns>
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
        /// <summary>
        /// Adds user into users list
        /// </summary>
        /// <param name="toAdd">User to add to list</param>
        /// <returns>True if login and username are not taken, false if not</returns>
        public bool AddLoged(Loged toAdd)
        {
            var users = GetLoged();
            
            if (!users.Select(x => x._login).Contains(toAdd._login) && 
                !users.Select(x => x._username).Contains(toAdd._username))
            {
                users.Add(toAdd);

                SaveUsers(users);

                return true;
            }
            return false;
        }
        /// <summary>
        /// Adds admin property to a user by username
        /// </summary>
        /// <param name="username">Username of an user to make admin</param>
        /// <returns>True if uesr exists, false if not</returns>
        public bool AddAdmin(string username)
        {
            var users = GetLoged();

            if (users.Select(x => x._username).Contains(username))
            {

                users.First(x => x._username == username)._isAdmin = true;

                SaveUsers(users);

                return true;
            }
            return false;
        }
        /// <summary>
        /// Saves users into a json file
        /// </summary>
        /// <param name="users">Users to save to a file</param>
        public void SaveUsers(IEnumerable<Loged> users)
        {

            File.WriteAllText(JsonFileName, string.Empty);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Loged>>(
                 new Utf8JsonWriter(outputStream, new JsonWriterOptions
                 {
                     SkipValidation = true,
                     Indented = true
                 }),
                users
            );
        }
    }
}
