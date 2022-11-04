using System;

namespace TaskrowSharp.Exceptions
{
    [Serializable]
    public class TaskrowException : Exception
    {
        public TaskrowException()
        {

        }

        public TaskrowException(string message)
            : base(message)
        {

        }

        public TaskrowException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
