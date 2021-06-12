using System;

namespace TaskrowSharp
{
    [Serializable]
    public class NotConnectedException : TaskrowException
    {
        public NotConnectedException()
            : base("Taskrow client not connected")
        {

        }

        public NotConnectedException(string message)
            : base(message)
        {

        }

        public NotConnectedException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
