namespace blogapp.Models
{
    public class Reaction
    {
        Loged _user { get; set; }
        int _reaction { get; set; }


        public Reaction(Loged user, int reaction)
        {
            _user = user;
            _reaction = reaction;
        }
    }
}
