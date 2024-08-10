using System;

namespace TaskrowSharp.Exceptions;

[Serializable]
public class TaskrowSharpException : Exception
{
    public TaskrowSharpException()
    {

    }

    public TaskrowSharpException(string message)
        : base(message)
    {

    }

    public TaskrowSharpException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
