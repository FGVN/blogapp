namespace blogapp.Models
{
    public class Admin : Loged
    {
        string _login;
        string _password;
        public Admin(string login, string password) : base (login, password)
        {
            _login = login;
            _password = password;
        }

        public bool AddAdmin(string login)
        {
            //search a list of registered users by login for adding an admin, deleting user from registered list and add into admin
            throw new NotImplementedException();
        }

        public void EditPost(Post toChange, string newText)
        {
            //already inserts with changed text;
            toChange.SetText(newText);
        }

        public void DeletePost(Post toDelete)
        {
            //search a list for a that post and delete it
        }
    }
}
