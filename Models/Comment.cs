namespace blogapp.Models
{
    public class Comment
    {
        public Loged _author { get; }
        public string _text { get; }
        public DateTime _postDate { get; }

        public Comment(string text, Loged author)
        {
            _author = author;
            _text = text;
            _postDate = DateTime.Now;
        }
    }
}
