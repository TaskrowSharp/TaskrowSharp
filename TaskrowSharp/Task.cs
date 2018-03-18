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

        public TaskSituation TaskSituation { get; set; }

        public Task(int taskNumber, int taskID, string taskTitle, DateTime creationDate, DateTime dueDate,
            int jobID, int jobNumber, string clientNickName, int ownerUserID, TaskSituation taskSituation)
        {
            this.TaskNumber = taskNumber;
            this.TaskID = taskID;
            this.TaskTitle = taskTitle;
            this.CreationDate = creationDate;
            this.DueDate = dueDate;
            this.JobID = jobID;
            this.JobNumber = jobNumber;
            this.ClientNickName = clientNickName;
            this.OwnerUserID = ownerUserID;
            this.TaskSituation = taskSituation;
        }

        internal Task(ApiModels.TaskApi taskApi, TaskSituation taskSituation)
        {
            this.TaskNumber = taskApi.TaskNumber;
            this.TaskID = taskApi.TaskID;
            this.TaskTitle = taskApi.TaskTitle;
            this.CreationDate = Utils.Parser.ToDateTimeFromTaskrowDate(taskApi.CreationDate);
            this.DueDate = Utils.Parser.ToDateTimeFromTaskrowDate(taskApi.DueDate);
            this.JobID = taskApi.JobID;
            this.JobNumber = taskApi.JobNumber;
            this.ClientNickName = taskApi.ClientNickName;
            this.OwnerUserID = taskApi.OwnerUserID;
            this.TaskSituation = taskSituation;
        }
    }
}
