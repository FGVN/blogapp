namespace blogapp.Models
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    public class Loged 
    {
        public string _username { get; set; }

        public string _login { get; set; }

        public string _password { get; set; }
        public Loged(string _username, string _login, string _password)
        {
            this._username = _username;
            this._login = _login;
            this._password = _password;
        }

        public void Comment(string tex, Article toComment)
        {
            //same way as reacting
            throw new NotImplementedException();
        }

        public void Reaction(int reaction, Article toReact)
        {
            //sets reaction on post
            throw new NotImplementedException();
        }

        public void Logout()
        {
            //switches role to unloged
            throw new NotImplementedException();
        }


        public override string ToString() => JsonSerializer.Serialize<Loged>(this);
    }
}
