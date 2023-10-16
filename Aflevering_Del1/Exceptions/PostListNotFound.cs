namespace Aflevering_Del1.Exceptions;

public class PostListNotFound : Exception
{
    public PostListNotFound()
    {
    }
    public PostListNotFound(string message) : base(message)
    {
    }
}