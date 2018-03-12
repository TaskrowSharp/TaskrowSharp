using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    [Serializable]
    public class NotConnectedException : TaskrowException
    {
        public NotConnectedException()
            : base("Taskrow client not connected")
        {

        }
    }
}
