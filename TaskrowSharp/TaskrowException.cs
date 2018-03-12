using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    [Serializable]
    public class TaskrowException : System.Exception
    {
        public TaskrowException()
        {

        }

        public TaskrowException(string message) 
            : base(message)
        {

        }

        public TaskrowException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
