namespace Recipe.Core.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string? message = "Bad Request") : base(message)
        {
        }
    }
}