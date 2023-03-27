namespace blogapp.Models
{
    public class Article
    {
        public int _id { get; set; }
        public string _header { get; set; }
        public string _about { get; set; }
        public string _title { get; set; }
        public string _text { get; set; }

        public Admin _author     { get; }
        public DateTime _postDate { get; }
        public int _viewcount { get; set; }


        public Article(string header, string about, string text, Admin author)
        {
            _header = header;
            _about = about;
            _text = text;
            _author = author;
            _postDate = DateTime.Now;
        } 

        public Article(string title, string text, string username, DateTime postDate)
        {

        }

    }
}
