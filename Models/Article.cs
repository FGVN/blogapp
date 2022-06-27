namespace blogapp.Models
{
    public class Article
    {
        public string _title { get; set; }
        public string _text { get; set; }

        public Admin _creator { get; }
        public DateTime _postDate { get; }
        public List<Comment> _comments { get; set; }
        public List<Reaction> _reactions { get; set; }


        public Article(string title, string text, Admin creator)
        {
            _title = title;
            _text = text;
            _creator = creator;
            _reactions = new List<Reaction>();
            _comments = new List<Comment>();
            _postDate = DateTime.Now;
        } 

        public Article(string title, string text, string username, DateTime postDate)
        {

        }

    }
}
