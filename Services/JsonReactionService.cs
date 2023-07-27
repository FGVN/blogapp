using blogapp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blogapp.Services
{
    public class JsonReactionService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonReactionService(IWebHostEnvironment webHostEnvironment) => WebHostEnvironment = webHostEnvironment;
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "reactions.json"); }
        }
        public List<Reaction> GetReactions()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var reacts = JsonSerializer.Deserialize<Reaction[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if (reacts.Any())
                return reacts.ToList();
            return new List<Reaction>();
        }

        public void AddReaction(Reaction toAdd)
        {
            // Console.WriteLine("ReactAdd");
            Console.WriteLine("React: " + toAdd._username + toAdd._article_id + toAdd._reaction);
            var reacts = GetReactions();
            reacts.Add(toAdd);

            File.WriteAllText(JsonFileName, string.Empty);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Reaction>>(
                 new Utf8JsonWriter(outputStream, new JsonWriterOptions
                 {
                     SkipValidation = true,
                     Indented = true
                 }),
                reacts
            );
        }

        public void RemoveReaction(Reaction toRemove)
        {
            Console.WriteLine("RemoveReact");
            var reacts = GetReactions();
            var update = reacts.Where(x => !(x._article_id == toRemove._article_id 
            && x._username == toRemove._username
            && x._reaction == toRemove._reaction));

            File.WriteAllText(JsonFileName, string.Empty);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Reaction>>(
                 new Utf8JsonWriter(outputStream, new JsonWriterOptions
                 {
                     SkipValidation = true,
                     Indented = true
                 }),
                update
            );
        }
    }
}
