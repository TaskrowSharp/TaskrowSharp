using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp
{
    public class SaveTaskResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        //public Task Entity { get; set; }

        public string TargetURL { get; set; }

        public string PreviousGUID { get; set; }

        public string UserTaskListGUID { get; set; }
    }
}
