namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class MakeFriendsCommand
    {
        // MakeFriends <username1> <username2>
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();
            string username1 = data[0];
            string username2 = data[1];

            if (Authorization.Instance.CurrentUser.Username != username1)
            {
                throw new InvalidOperationException("You can add friends only to your profile!");
            }
            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Users.Any(u => u.Username == username1))
                {
                    throw new ArgumentException($"{username1} not found!");
                }

                if (!context.Users.Any(u => u.Username == username2))
                {
                    throw new ArgumentException($"{username2} not found!");
                }

                User user1 = context.Users.FirstOrDefault(u => u.Username == username1);

                if (user1.Friends.Any(u => u.Username == username2))
                {
                    throw new InvalidOperationException($"{username2} is already a friend to {username1}.");
                }

                User user2 = context.Users.FirstOrDefault(u => u.Username == username2);

                user1.Friends.Add(user2);
                user2.Friends.Add(user1);
                context.SaveChanges();
            }

            return $"Friend {username2} added to {username1}";
        }
    }
}
