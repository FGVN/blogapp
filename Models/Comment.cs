namespace blogapp.Models
{
    public class Comment
    {
        public int _article_id { get; set; }
        public string _username { get; set; }
        public string _text { get; set; }

        public List<Comment> _replies { get; set; }
        public DateTime _postDate { get; set; }

        public Comment(int _article_id ,string _username, string _text)
        {
            this._article_id = _article_id;
            this._username = _username;
            this._text = _text;
            _postDate = DateTime.Now;
            _replies = new List<Comment>();
        }
    }
}
