namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();

            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Towns.Any(t => t.Name == townName))
                {
                    throw new ArgumentException($"Town {townName} already added!");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return "Town " +  townName + " was added successfully!";

            }
        }
    }
}
