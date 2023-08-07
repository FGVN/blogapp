namespace blogapp.Models
{
    /// <summary>
    /// Class that represents articles
    /// </summary>
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

        /// <summary>
        /// Constructor for an article class
        /// </summary>
        /// <param name="_header">Articles header</param>
        /// <param name="_about">Articles about</param>
        /// <param name="_title">Articles title that will be used as page name</param>
        /// <param name="_text">Text of an article</param>
        /// <param name="_author">Username of the user who created article</param>
        /// <param name="_postDate">Date of article posting</param>
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
