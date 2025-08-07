namespace Microscope.Boilerplate.Framework.Exceptions;

public class PoliciesException : Exception
{
    public PoliciesException()
    { }

    public PoliciesException(string message): base(message)
    { }

    public PoliciesException(string message, Exception innerException): base(message, innerException)
    { }
}    
