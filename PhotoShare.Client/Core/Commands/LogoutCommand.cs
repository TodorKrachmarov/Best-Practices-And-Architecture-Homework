namespace PhotoShare.Client.Core.Commands
{
    using Utilities;

    public class LogoutCommand
    {
        public string Execute()
        {
            string username = Authorization.Instance.Logout();

            return $"User {username} successfully logged out!";
        }
    }
}
