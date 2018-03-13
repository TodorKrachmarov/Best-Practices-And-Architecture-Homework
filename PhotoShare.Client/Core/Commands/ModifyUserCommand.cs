namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();

            string userName = data[0];
            string property = data[1];
            string value = data[2];

            if (Authorization.Instance.CurrentUser.Username != userName)
            {
                throw new InvalidOperationException("You can modify only your profile!");
            }
            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Users.Any(u => u.Username == userName))
                {
                    throw new ArgumentException($"User {userName} not found!");
                }

                User user = context.Users.FirstOrDefault(u => u.Username == userName);

                switch (property)
                {
                    case "Password":
                        user.Password = value;
                        context.SaveChanges();
                        break;
                    case "BornTown":
                        if (!context.Towns.Any(t => t.Name == value))
                        {
                            throw new ArgumentException($"Value {value} not valid!\nTown {value} not found!");
                        }
                        Town town = context.Towns.FirstOrDefault(t => t.Name == value);
                        user.BornTown = town;
                        context.SaveChanges();
                        break;
                    case "CurrentTown":
                        if (!context.Towns.Any(t => t.Name == value))
                        {
                            throw new ArgumentException($"Value {value} not valid!\nTown {value} not found!");
                        }
                        Town town1 = context.Towns.FirstOrDefault(t => t.Name == value);
                        user.CurrentTown = town1;
                        context.SaveChanges();
                        break;
                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                }
            }

            return "User " + userName + " " + property + " is " + value + ".";
        }
    }
}
