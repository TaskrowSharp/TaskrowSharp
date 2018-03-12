using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class SaveTaskRequest
    {
        public int JobNumber { get; set; }

        public string ClientNickName { get; set; }

        public int LastTaskItemID { get; set; }

        public int TaskID { get; set; }

        public string MemberListString { get; set; }
                        
        public int TaskNumber { get; set; }

        public string RowVersion { get; set; }
        
        public string TaskTitle { get; set; }
        
        public string TaskItemComment { get; set; }

        public int OwnerUserID { get; set; }

        public int SpentTime { get; set; }
        
        public DateTime DueDate { get; set; }

        public int PercentComplete { get; set; }
    }
}
