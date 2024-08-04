namespace Recipe.Core.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(string? message = "Something Went Wrong") : base(message)
        {
        }
    }
}