using System;

namespace TaskrowSharp.Exceptions
{
    [Serializable]
    public class TaskrowWebException : TaskrowException
    {
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }

        public TaskrowWebException()
            : base("TaskrowSharp WebException")
        {

        }

        public TaskrowWebException(string message)
            : base(message)
        {

        }

        public TaskrowWebException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public TaskrowWebException(System.Net.HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public TaskrowWebException(System.Net.HttpStatusCode httpStatusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
