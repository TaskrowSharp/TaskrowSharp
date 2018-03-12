using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class SubTask
    {
        public int SubtaskID { get; set; }

        public int TaskID { get; set; }

        public int ChildTaskID { get; set; }

        public string Title { get; set; }

        public TaskDetail ChildTask { get; set; }
    }
}
