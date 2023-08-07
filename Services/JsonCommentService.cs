using blogapp.Models;
using System.Text.Json;
using System.Xml.Linq;

namespace blogapp.Services
{
    /// <summary>
    /// Service that operates with json with comment data
    /// </summary>
    public class JsonCommentService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonCommentService(IWebHostEnvironment webHostEnvironment) => WebHostEnvironment = webHostEnvironment;
        /// <summary>
        /// Configuring path to json file
        /// </summary>
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "comments.json"); 
        /// <summary>
        /// Gets list of all comments in json file
        /// </summary>
        /// <returns>List of all comments or new list</returns>
        public List<Comment> GetComments()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            
            var json = JsonSerializer.Deserialize<List<Comment>>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if(json != null)
                return json;
            return new List<Comment>();
        }
        /// <summary>
        /// Adds comment to the json
        /// </summary>
        /// <param name="toAdd">Comment to add to list</param>
        public void AddComment(Comment toAdd)
        {
            var comments = GetComments().ToList();

            comments.Add(toAdd);

            SaveComments(comments);
        }
        /// <summary>
        /// Adds a comment into some comments list
        /// </summary>
        /// <param name="toAdd">Comment to add reply to</param>
        /// <param name="Reply">Reply comment</param>
        public void AddReply(Comment toAdd, Comment Reply)
        {
            var comments = GetComments().ToList();

            comments.Where(x => x._username == toAdd._username).First(x => x._postDate == toAdd._postDate)._replies.Add(Reply);

            SaveComments(comments);
        }
        /// <summary>
        /// Saves comments into the json file
        /// </summary>
        /// <param name="comments">IEnumerable<Comments> to save to json</param>
        public void SaveComments(IEnumerable<Comment> comments)
        {
            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Comment>>(
                 new Utf8JsonWriter(outputStream, new JsonWriterOptions
                 {
                     SkipValidation = true,
                     Indented = true
                 }),
                comments
            );
        }
    }
}
