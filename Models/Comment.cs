namespace blogapp.Models
{
    public class Comment
    {
        Loged _author;
        string _text;
        DateTime _postDate;

        public Comment(string text, Loged author)
        {
            _author = author;
            _text = text;
            _postDate = DateTime.Now;
        }
    }
}
