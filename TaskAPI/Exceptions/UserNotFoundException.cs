namespace TaskAPI.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int userId) : base($"The user with the ID : {userId} doesn't exist.")
        {
        }
    }
}
