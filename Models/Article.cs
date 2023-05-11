namespace blogapp.Models
{
    public class Article
    {
        public static int _amount = 0;
        public int _id { get; set; }
        public string _header { get; set; }
        public string _about { get; set; }
        public string _title { get; set; }
        public string _text { get; set; }
        public string _author { get; set; }
        public DateTime _postDate { get; set; }
        public int _viewcount { get; set; }


        public Article(string _header, string _about, string _title, string _text, string _author, DateTime _postDate)
        {
            _amount = System.Threading.Interlocked.Increment(ref _amount);
            this._id = _amount; 
            this._header = _header;
            this._about = _about;
            this._title = _title;
            this._text = _text;
            this._author = _author;
            this._postDate = _postDate;
            _viewcount = 0;
        } 

    }
}
