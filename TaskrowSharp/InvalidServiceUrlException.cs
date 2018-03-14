using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    [Serializable]
    public class InvalidServiceUrlException : TaskrowException
    {
        public InvalidServiceUrlException()
            : base("Invalid Service Url")
        {

        }

        public InvalidServiceUrlException(string message)
            : base(message)
        {

        }

        public InvalidServiceUrlException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
