using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class TaskDetail
    {
        public int TaskID { get; set; }

        public int TaskNumber { get; set; }

        public string TaskTitle { get; set; }

        public int JobID { get; set; }

        public int JobNumber { get; set; }

        public string JobTitle { get; set; }

        public string MemberListString { get; set; }

        public List<TaskItem> TaskItems { get; set; }
        
        public List<TaskTag> Tags { get; set; }

        public string ClientNickName { get; set; }

        public string RowVersion { get; set; }

        public DateTime DueDate { get; set; }
        
        public List<SubTask> SubTasks { get; set; }

        public TaskDetail()
        {
            this.TaskItems = new List<TaskItem>();
            this.Tags = new List<TaskTag>();
            this.SubTasks = new List<SubTask>();
        }
    }
}
