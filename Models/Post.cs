namespace blogapp.Models
{
    public class Post
    {
        string _title;
        string _text;
        string _path;
        int _likes;
        int _dislikes;
        Admin _creator;
        DateTime _postDate;
        List<Comment> _comments;


        public Post(string title, string text, Admin creator)
        {
            _title = title;
            _text = text;
            _creator = creator;
            _path = "/" + title;
            _comments = new List<Comment>();
            _postDate = DateTime.Now;
        } 

        public string GetTitle() => _title;
        public string GetText() => _text;

        public void SetText(string text) => _text = text;
        public string GetPath() => _path;
        public int GetLikes() => _likes;

        public int GetDisikes() => _dislikes;

        public Admin GetCreator() => _creator;

        public List<Comment> GetComments() => _comments;

    }
}
