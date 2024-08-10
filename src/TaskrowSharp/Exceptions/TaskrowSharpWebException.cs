using System;

namespace TaskrowSharp.Exceptions;

[Serializable]
public class TaskrowSharpWebException : TaskrowSharpException
{
    public System.Net.HttpStatusCode HttpStatusCode { get; set; }

    public TaskrowSharpWebException(System.Net.HttpStatusCode httpStatusCode, string message)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}
