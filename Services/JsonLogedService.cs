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

        public IEnumerable<Loged> GetLoged()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Loged[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public bool AddLoged(Loged toAdd)
        {
            IEnumerable<Loged> users = GetLoged();
            if (users.First(x => x._login == toAdd._login) == null && users.First(x => x._username == toAdd._username) == null)
            {
                users.ToList().Add(toAdd); 
                using (var outputStream = File.OpenWrite(JsonFileName))
                {
                    JsonSerializer.Serialize<IEnumerable<Loged>>(
                        new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        users
                    );
                }
                return true;
            }
            return false;
        }
    }
}
