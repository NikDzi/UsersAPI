namespace TaskAPI.Exceptions
{
    public sealed class PermissionNotFoundException : NotFoundException
    {
        public PermissionNotFoundException(int permissionID) : base($"The user with the ID : {permissionID} doesn't exist.")
        {

        }
    }
}
