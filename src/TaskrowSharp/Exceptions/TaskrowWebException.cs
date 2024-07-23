using System;

namespace TaskrowSharp.Exceptions;

[Serializable]
public class TaskrowWebException : TaskrowException
{
    public System.Net.HttpStatusCode HttpStatusCode { get; set; }

    public TaskrowWebException(System.Net.HttpStatusCode httpStatusCode, string message)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}
