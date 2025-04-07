namespace DAL.Exceptions;

public class TooManyRequestException : Exception
{
    public TooManyRequestException() : base()
    {

    }

    public TooManyRequestException(string message) : base(message)
    {
    }

    public TooManyRequestException(string message, Exception inner) : base(message, inner)
    {
    }
    
    protected TooManyRequestException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    { }
}