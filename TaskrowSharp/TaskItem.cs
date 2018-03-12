using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class TaskItem
    {
        public int TaskItemID { get; set; }

        public int OldOwnerUserID { get; set; }

        public string OldOwnerName { get; set; }

        public int NewOwnerUserID { get; set; }

        public string NewOwnerName { get; set; }

        public string TaskItemComment { get; set; }
    }
}
