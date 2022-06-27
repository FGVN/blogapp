namespace blogapp.Models
{
    public class Admin : Loged
    {
        public string _username { get; set; }
        public string _login { get; set; }
        string _password;
        public Admin(string username, string login, string password) : base (username, login, password)
        {
            _username = username;   
            _login = login;
            _password = password;
        }

        public bool AddAdmin(string login)
        {
            //search a list of registered users by login for adding an admin, deleting user from registered list and add into admin
            throw new NotImplementedException();
        }

        public void EditPost(Article toChange, string newText)
        {
            //already inserts with changed text;
            //toChange.SetText(newText);
        }

        public void DeletePost(Article toDelete)
        {
            //search a list for a that post and delete it
        }
    }
}
