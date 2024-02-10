using System;

namespace TaskrowSharp.Exceptions;

[Serializable]
public class InvalidServiceUrlException : TaskrowException
{
    public InvalidServiceUrlException()
        : base("Invalid Service Url")
    {

    }

    public InvalidServiceUrlException(string message)
        : base(message)
    {

    }

    public InvalidServiceUrlException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
