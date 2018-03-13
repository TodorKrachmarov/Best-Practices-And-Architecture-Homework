namespace PhotoShare.Client.Core
{
    using Commands;
    using System;
    using System.Linq;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {

            string command = commandParameters[0];
            commandParameters = commandParameters.Skip(1).ToArray();
            string result = string.Empty;

            switch (command)
            {
                case "RegisterUser":
                    RegisterUserCommand regUser = new RegisterUserCommand();
                    result = regUser.Execute(commandParameters);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute();
                    break;
                case "AddTown":
                    AddTownCommand addTown = new AddTownCommand();
                    result = addTown.Execute(commandParameters);
                    break;
                case "ModifyUser":
                    ModifyUserCommand modifyUser = new ModifyUserCommand();
                    result = modifyUser.Execute(commandParameters);
                    break;
                case "DeleteUser":
                    DeleteUser deleteUser = new DeleteUser();
                    result = deleteUser.Execute(commandParameters);
                    break;
                case "AddTag":
                    AddTagCommand addTag = new AddTagCommand();
                    result = addTag.Execute(commandParameters);
                    break;
                case "CreateAlbum":
                    CreateAlbumCommand createAlbum = new CreateAlbumCommand();
                    result = createAlbum.Execute(commandParameters);
                    break;
                case "AddTagTo":
                    AddTagToCommand addTagTo = new AddTagToCommand();
                    result = addTagTo.Execute(commandParameters);
                    break;
                case "MakeFriends":
                    MakeFriendsCommand makeFriends = new MakeFriendsCommand();
                    result = makeFriends.Execute(commandParameters);
                    break;
                case "ListFriends":
                    PrintFriendsListCommand listFriends = new PrintFriendsListCommand();
                    result = listFriends.Execute(commandParameters);
                    break;
                case "ShareAlbum":
                    ShareAlbumCommand shareAlbum = new ShareAlbumCommand();
                    result = shareAlbum.Execute(commandParameters);
                    break;
                case "UploadPicture":
                    UploadPictureCommand uploadPicture = new UploadPictureCommand();
                    result = uploadPicture.Execute(commandParameters);
                    break;
                case "Login":
                    LoginCommand login = new LoginCommand();
                    result = login.Execute(commandParameters);
                    break;
                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute();
                    break;
                default:
                    throw new InvalidOperationException($"Command {command} not valid!");
            }

            return result;
        }
    }
}
