namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            string result = string.Empty;
            string username = data[0];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Users.Any(u => u.Username == username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                var users = context.Users.FirstOrDefault(u => u.Username == username);
                if (users.Friends.Count == 0)
                {
                    result = $"No friends for this user.";
                }
                else
                {
                    result = $"Friends:";
                    foreach (var u in users.Friends)
                    {
                        result += $"\n-{u.Username}";
                    }
                }
            }
            return result;

        }
    }
}
