namespace blogapp.Models
{
    public class Reaction
    {
        public int _article_id { get; set; }
        string _username { get; set; }
        int _reaction { get; set; }


        public Reaction(int article_id,string username, int reaction)
        {
            _article_id = article_id;
            _username = username;
            _reaction = reaction;
        }
    }
}
