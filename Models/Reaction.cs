namespace blogapp.Models
{
    /// <summary>
    /// Class that represents reactions on an article
    /// </summary>
    public class Reaction
    {
        public int _article_id { get; set; }
        public string _username { get; set; }
        public int _reaction { get; set; }

        /// <summary>
        /// Constructor for reaction
        /// </summary>
        /// <param name="_article_id">Id of article to add reaction to</param>
        /// <param name="_username">Name of the user who reacted</param>
        /// <param name="_reaction">Reaction that has been sent</param>
        public Reaction(int _article_id,string _username, int _reaction)
        {
            this._article_id = _article_id;
            this._username = _username;
            this._reaction = _reaction;
        }
    }
}
