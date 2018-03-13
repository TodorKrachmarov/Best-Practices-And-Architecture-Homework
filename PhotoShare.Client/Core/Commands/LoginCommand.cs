namespace PhotoShare.Client.Core.Commands
{
    using Utilities;

    public class LoginCommand
    {
            //Login <username> <password>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];

            Authorization.Instance.Login(username, password);


            return $"User {username} successfully logged in!";
        }
    }
}
