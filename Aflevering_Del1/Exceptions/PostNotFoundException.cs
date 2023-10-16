namespace Aflevering_Del1.Exceptions;

public class PostNotFoundException : Exception
{
    public PostNotFoundException()
    {
    }
    public PostNotFoundException(string message) : base(message)
    {
    }
}