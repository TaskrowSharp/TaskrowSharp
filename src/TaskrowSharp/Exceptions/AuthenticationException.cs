using System;

namespace TaskrowSharp.Exceptions
{
    [Serializable]
    public class AuthenticationException : TaskrowException
    {
        public AuthenticationException()
            : base("Authentication Error")
        {

        }

        public AuthenticationException(string message)
            : base(message)
        {

        }

        public AuthenticationException(string message, Exception baseException)
            : base(message, baseException)
        {

        }
    }
}
