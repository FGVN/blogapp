using blogapp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blogapp.Services
{
    public class JsonAdminService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonAdminService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "admins.json"); }
        }

        public IEnumerable<Admin> GetAdmins()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Admin[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public bool AddAdmin(Loged toAdd)
        {

            Admin admin = new Admin(toAdd._username, toAdd._login, toAdd._password);
            var admins = GetAdmins();

            if(admins.First(x => x._login == toAdd._login) == null && admins.First(x => x._username == toAdd._username) == null)
            {
                using (var outputStream = File.OpenWrite(JsonFileName))
                {
                    JsonSerializer.Serialize<IEnumerable<Loged>>(
                        new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        admins
                    );
                }
                return true;
            }
            return false;
        }
    }
}
