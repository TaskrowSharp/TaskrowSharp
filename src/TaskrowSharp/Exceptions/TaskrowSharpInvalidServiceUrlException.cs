using System;

namespace TaskrowSharp.Exceptions;

[Serializable]
public class TaskrowSharpInvalidServiceUrlException : TaskrowSharpException
{
    public TaskrowSharpInvalidServiceUrlException()
        : base("Invalid Service Url")
    {

    }

    public TaskrowSharpInvalidServiceUrlException(string message)
        : base(message)
    {

    }

    public TaskrowSharpInvalidServiceUrlException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
