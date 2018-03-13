namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class AddTagToCommand
    {
        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();
            string albumName = data[0];
            string tagName = data[1].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Albums.Any(a => a.Name == albumName) || !context.Tags.Any(t => t.Name == tagName))
                {
                    throw new ArgumentException($"Either {tagName} or {albumName} do not exist!");
                }

                Album album = context.Albums.FirstOrDefault(a => a.Name == albumName);

                string username = Authorization.Instance.CurrentUser.Username;

                if (!album.AlbumRoles.Any(ar => ar.User.Username == username && ar.Role == Role.Owner))
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                Tag tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

                album.Tags.Add(tag);
                context.SaveChanges();
            }

            return $"Tag {tagName} added to {albumName}!";
        }
    }
}
