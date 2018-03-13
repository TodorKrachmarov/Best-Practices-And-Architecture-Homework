namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class AddTagCommand
    {
        // AddTag <tag>
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();

            string tag = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Tags.Any(t => t.Name == tag))
                {
                    throw new ArgumentException($"Tag {tag} exists!");
                }

                context.Tags.Add(new Tag
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            return $"Tag {tag} was added successfully!";
        }
    }
}
