using blogapp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blogapp.Services
{
    public class JsonLogedService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonLogedService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "loged.json"); }
        }

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

        public bool AddLoged(Loged toAdd)
        {
            var users = GetLoged();
            
            if (!users.Select(x => x._login).Contains(toAdd._login) && 
                !users.Select(x => x._username).Contains(toAdd._username))
            {

                users.Add(toAdd);
                using var outputStream = File.OpenWrite(JsonFileName);

                JsonSerializer.Serialize<IEnumerable<Loged>>(
                     new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    users
                );
                
                return true;
            }
            return false;
        }

        public bool AddAdmin(string username)
        {
            var users = GetLoged();

            if (users.Select(x => x._username).Contains(username))
            {

                users.First(x => x._username == username)._isAdmin = true;

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

                return true;
            }
            return false;
        }
    }
}
