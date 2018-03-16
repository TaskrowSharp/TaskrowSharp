using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp
{
    [Serializable]
    public class TaskrowWebException : TaskrowException
    {
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }

        public TaskrowWebException()
            : base("Taskrow WebException")
        {

        }

        public TaskrowWebException(string message)
            : base(message)
        {

        }

        public TaskrowWebException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }

        public TaskrowWebException(System.Net.HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
        }

        public TaskrowWebException(System.Net.HttpStatusCode httpStatusCode, string message, System.Exception innerException)
            : base(message, innerException)
        {
            this.HttpStatusCode = httpStatusCode;
        }
    }
}
