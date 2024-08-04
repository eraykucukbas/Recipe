namespace Recipe.Core.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string? message = "Authorization Exception") : base(message)
        {
        }
    }
}