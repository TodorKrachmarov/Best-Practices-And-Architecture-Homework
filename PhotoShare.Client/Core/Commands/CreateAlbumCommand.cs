namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        // CreateAlbum admin HotPics Blue #sex
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();
            string username = data[0];
            string albumTitle = data[1];
            Color color = Color.Black;
            try
            {
                color = (Color)Enum.Parse(typeof(Color), data[2]);

            }
            catch
            {

                throw new ArgumentException($"Color {data[2]} not found!");
            }
            List<string> tagNames = new List<string>();
            for (int i = 3; i < data.Length; i++)
            {
                string tag = data[i].ValidateOrTransform();
                tagNames.Add(tag);
            }
            List<Tag> tags = new List<Tag>();
            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Users.Any(u => u.Username == username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                if (context.Albums.Any(a => a.Name == albumTitle))
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }
                for (int i = 0; i < tagNames.Count; i++)
                {
                    string tagName = tagNames[i];

                    if (!context.Tags.Any(t => t.Name == tagName))
                    {
                        throw new ArgumentException("Invalid tags!!");
                    }
                    Tag tag = context.Tags.FirstOrDefault(t => t.Name == tagName);
                    tags.Add(tag);
                }

                Album album = new Album();
                album.Name = albumTitle;
                album.BackgroundColor = color;
                album.IsPublic = false;
                album.Tags = tags;

                User user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user.IsDeleted == true)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                AlbumRole albumRole = new AlbumRole();
                albumRole.User = user;
                albumRole.Role = Role.Owner;
                albumRole.Album = album;

                context.AlbumRoles.Add(albumRole);
                context.SaveChanges();

            }


                return $"Album {albumTitle} successfully created!";
        }
    }
}
