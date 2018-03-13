namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();
            int albumId = int.Parse(data[0]);
            string username = data[1];
            Role role = Role.Viewer;
            try
            {
                role = (Role)Enum.Parse(typeof(Role), data[2]);

            }
            catch
            {

                throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
            }

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Albums.Any(a => a.Id == albumId))
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                if (!context.Users.Any(u => u.Username == username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                Album album = context.Albums.FirstOrDefault(a => a.Id == albumId);

                string usernameLogged = Authorization.Instance.CurrentUser.Username;

                if (!album.AlbumRoles.Any(ar => ar.User.Username == usernameLogged && ar.Role == Role.Owner))
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                User user = context.Users.FirstOrDefault(u => u.Username == username);

                AlbumRole albumRole = new AlbumRole();
                albumRole.Album = album;
                albumRole.User = user;
                albumRole.Role = role;

                album.AlbumRoles.Add(albumRole);
                context.SaveChanges();
            }

                return $"Username {username} added to album {albumId} ({role})";
        }
    }
}
