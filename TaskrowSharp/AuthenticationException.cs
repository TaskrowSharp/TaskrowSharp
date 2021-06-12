using System;

namespace TaskrowSharp
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

        public AuthenticationException(string message, System.Exception baseException)
            : base(message, baseException)
        {

        }
    }
}
