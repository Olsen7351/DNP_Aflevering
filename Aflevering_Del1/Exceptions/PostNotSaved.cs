namespace Aflevering_Del1.Exceptions;

public class PostNotSaved : Exception
{
    public PostNotSaved()
    {
    }
    public PostNotSaved(string message) : base(message)
    {
    }
}