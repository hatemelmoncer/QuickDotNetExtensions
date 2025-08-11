namespace QuickDotNetExtensions;

[Serializable]
public class StringNotFoundException : Exception
{
    public StringNotFoundException()
    {
    }

    public StringNotFoundException(string? message) : base(message)
    {
    }
}