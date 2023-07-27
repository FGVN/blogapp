namespace blogapp.Models
{
    public class Reaction
    {
        public int _article_id { get; set; }
        public string _username { get; set; }
        public int _reaction { get; set; }


        public Reaction(int _article_id,string _username, int _reaction)
        {
            this._article_id = _article_id;
            this._username = _username;
            this._reaction = _reaction;
        }
    }
}
