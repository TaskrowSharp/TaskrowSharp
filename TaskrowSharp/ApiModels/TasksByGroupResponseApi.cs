using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class TasksByGroupResponseApi
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TasksByGroupEntityApi Entity { get; set; }
        //public object TargetURL { get; set; }
    }
}
