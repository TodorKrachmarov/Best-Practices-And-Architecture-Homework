namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Utilities;

    public class DeleteUser
    {
        // DeleteUser <username>
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();

            string username = data[0];

            if (Authorization.Instance.CurrentUser.Username != username)
            {
                throw new InvalidOperationException("You can only delete your profile!");
            }

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                if (user.IsDeleted == true)
                {
                    throw new InvalidOperationException($"User {username} is already deleted!");
                }

                user.IsDeleted = true;
                context.SaveChanges();

                Authorization.Instance.Logout();

                return $"User {username} was deleted successfully!";
            }
        }
    }
}
