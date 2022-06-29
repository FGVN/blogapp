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
            var s = new List<Loged>();
            var k = JsonSerializer.Deserialize<List<Loged>>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if(k != null)
                return k;
            return s;
        }

        public bool AddLoged(Loged toAdd)
        {
            var users = GetLoged();
            
            if (users.Select(x => x._login).Contains(toAdd._login) == false && users.Select(x => x._username).Contains(toAdd._username) == false)
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
    }
}
