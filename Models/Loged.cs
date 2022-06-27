namespace blogapp.Models
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    public class Loged 
    {
        [JsonPropertyName("_username")]
        public string _username { get; set; }
        [JsonPropertyName("_login")]
        public string _login { get; set; }
        [JsonPropertyName("_password")]
        public string _password { get; set; }
        public Loged(string username, string login, string password)
        {
            _username = username;
            _login = login;
            _password = password;
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
