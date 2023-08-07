namespace blogapp.Models
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    /// <summary>
    /// Class that represents registered users
    /// </summary>
    public class Loged 
    {
        public bool _isAdmin { get; set; }
        public string _username { get; set; }
        public string _login { get; set; }
        public string _password { get; set; }
        public Loged(string _username, string _login, string _password)
        {
            this._username = _username;
            this._login = _login;
            this._password = _password;
            _isAdmin = false;
        }

        public override string ToString() => JsonSerializer.Serialize<Loged>(this);
    }
}
