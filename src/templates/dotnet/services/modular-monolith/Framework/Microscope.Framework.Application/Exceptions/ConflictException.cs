namespace Microscope.Boilerplate.Framework.Exceptions;

public class ConflictException : Exception
{
    public ConflictException()
    { }

    public ConflictException(string message): base(message)
    { }

    public ConflictException(string message, Exception innerException): base(message, innerException)
    { }
}    
