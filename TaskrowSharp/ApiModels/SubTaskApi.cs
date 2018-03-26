using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class SubTaskApi
    {
        public int SubTaskID { get; set; }

        public int TaskID { get; set; }

        public int? ChildTaskID { get; set; }

        public string Title { get; set; }

        public ApiModels.TaskDataApi ChildTask { get;set; }
    }
}
