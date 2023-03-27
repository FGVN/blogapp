namespace blogapp.Models
{
    public class Comment
    {
        public int _article_id { get; set; }
        public string _username { get; }
        public string _text { get; }
        public DateTime _postDate { get; }

        public Comment(int article_id ,string username, string text)
        {
            _article_id = article_id;
            _username = username;
            _text = text;
            _postDate = DateTime.Now;
        }
    }
}
