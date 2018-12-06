using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class TaskHeader
    {
        public int TaskNumber { get; set; }

        public int TaskID { get; set; }

        public string TaskTitle { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DueDate { get; set; }

        public int JobID { get; set; }

        public int JobNumber { get; set; }

        public string ClientNickname { get; set; }

        public int OwnerUserID { get; set; }

        public string TaskUrl { get { return string.Format("{0}/{1}/{2}", this.ClientNickname, this.JobNumber, this.TaskNumber); } }

        public TaskSituation TaskSituation { get; set; }

        public int EffortEstimationMinutes { get; set; }

        public bool Closed { get; set; }

        public List<String> Tags { get; set; }

        public TaskHeader(int taskNumber, int taskID, string taskTitle, DateTime creationDate, DateTime dueDate,
            int jobID, int jobNumber, string clientNickName, int ownerUserID, TaskSituation taskSituation, List<string> tags)
        {
            this.TaskNumber = taskNumber;
            this.TaskID = taskID;
            this.TaskTitle = taskTitle;
            this.CreationDate = creationDate;
            this.DueDate = dueDate;
            this.JobID = jobID;
            this.JobNumber = jobNumber;
            this.ClientNickname = clientNickName;
            this.OwnerUserID = ownerUserID;
            this.TaskSituation = taskSituation;
            this.Tags = tags;
        }

        internal TaskHeader(ApiModels.TaskHeaderApi taskHeaderApi, TaskSituation taskSituation)
        {
            this.TaskNumber = taskHeaderApi.TaskNumber;
            this.TaskID = taskHeaderApi.TaskID;
            this.TaskTitle = taskHeaderApi.TaskTitle;
            this.CreationDate = Utils.Parser.ToDateTimeFromTaskrowDate(taskHeaderApi.CreationDate);
            this.DueDate = Utils.Parser.ToDateTimeFromTaskrowDate(taskHeaderApi.DueDate);
            this.JobID = taskHeaderApi.JobID;
            this.JobNumber = taskHeaderApi.JobNumber;
            this.ClientNickname = taskHeaderApi.ClientNickName;
            this.OwnerUserID = taskHeaderApi.OwnerUserID;
            this.TaskSituation = taskSituation;
            this.EffortEstimationMinutes = taskHeaderApi.EffortEstimation;
            this.Closed = taskHeaderApi.Closed;

            this.Tags = new List<string>();
            if (!string.IsNullOrEmpty(taskHeaderApi.Tags))
                this.Tags = taskHeaderApi.Tags.Split(',').Select(a => a.Split('|')[0]).ToList();
        }
    }
}
