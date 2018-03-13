namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            Authorization.Instance.ValidateIsUserLoggedIn();
            string albumName = data[0];
            string picName = data[1];
            string picPath = data[2];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Albums.Any(a => a.Name == albumName))
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                Album album = context.Albums.FirstOrDefault(a => a.Name == albumName);

                string username = Authorization.Instance.CurrentUser.Username;

                if (!album.AlbumRoles.Any(ar => ar.User.Username == username && ar.Role == Role.Owner))
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                Picture pic = new Picture();
                pic.Title = picName;
                pic.Path = picPath;
                pic.Albums.Add(album);

                context.Pictures.Add(pic);
                context.SaveChanges();
            }

            return $"Picture {picName} added to {albumName}!";
        }
    }
}
