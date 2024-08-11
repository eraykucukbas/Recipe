namespace Recipe.Core.Exceptions
{
    public class AlreadyDefinedException : Exception
    {
        public AlreadyDefinedException(string? message = "Already Defined") : base(message)
        {
        }
    }
}