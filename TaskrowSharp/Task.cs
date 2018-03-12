using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class Task
    {
        public int TaskNumber { get; set; }

        public int TaskID { get; set; }

        public string TaskTitle { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DueDate { get; set; }

        public int JobID { get; set; }

        public int JobNumber { get; set; }

        public string ClientNickName { get; set; }

        public int OwnerUserID { get; set; }
        
        public string TaskUrl { get { return string.Format("/#taskcentral/{0}/{1}/{2}", this.ClientNickName, this.JobNumber, this.TaskNumber); } }
    }
}
