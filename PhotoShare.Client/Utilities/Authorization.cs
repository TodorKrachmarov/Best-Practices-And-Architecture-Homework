namespace PhotoShare.Client.Utilities
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class Authorization
    {
        private static Authorization instance;

        private User currentUser;
         
        private Authorization()
        {

        }

        public static Authorization Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Authorization();
                }
                return instance;
            }
        }

        public User CurrentUser
        {
            get
            {
                return this.currentUser;
            }
        }

        public bool Login(string username, string password)
        {
            if (this.currentUser != null)
            {
                throw new ArgumentException("You should logout first!");
            }
            using (var context = new PhotoShareContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password!");
                }
                else
                {
                    this.currentUser = user;
                    return true;
                }
            }
        }

        public string Logout ()
        {
            if (this.currentUser == null)
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }
            string username = this.currentUser.Username;
            this.currentUser = null;
            return username;
        }

        public void ValidateIsUserLoggedIn()
        {
            if (this.currentUser == null)
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
        }
    }
}
