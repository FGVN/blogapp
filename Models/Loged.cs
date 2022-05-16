namespace blogapp.Models
{
    public class Loged 
    {
        string _login;
        string _password;
        public Loged(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public void Comment(string tex, Post toComment)
        {
            //same way as reacting
            throw new NotImplementedException();
        }

        public void Reaction(int reaction, Post toReact)
        {
            //sets reaction on post
            throw new NotImplementedException();
        }

        public void Logout()
        {
            //switches role to unloged
            throw new NotImplementedException();
        }
    }
}
