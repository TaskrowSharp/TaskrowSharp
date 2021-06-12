using System;

namespace TaskrowSharp
{
    public class SaveTaskRequest
    {
        public int TaskID { get; set; }
        public int JobNumber { get; set; }
        public string ClientNickname { get; set; }
        public int LastTaskItemID { get; set; }
        public string MemberListString { get; set; }                      
        public int TaskNumber { get; set; }
        public string RowVersion { get; set; }    
        public string TaskTitle { get; set; }
        public string TaskItemComment { get; set; }
        public int OwnerUserID { get; set; }
        public DateTime DueDate { get; set; }
        public int SpentTimeMinutes { get; set; }
        public int EffortEstimationMinutes { get; set; }

        public SaveTaskRequest(
            int taskID, 
            string clientNickname, 
            int jobNumber, 
            int taskNumber, 
            string taskTitle, 
            string taskItemComment,
            int ownerUserID,
            string rowVersion, 
            int lastTaskItemID,
            DateTime dueDate,
            int spendTimeMinutes,
            int effortEstimationMinutes)
        {
            this.TaskID = taskID;
            this.ClientNickname = clientNickname;
            this.JobNumber = jobNumber;
            this.TaskNumber = taskNumber;
            this.TaskTitle = taskTitle;
            this.TaskItemComment = taskItemComment;
            this.OwnerUserID = ownerUserID;
            this.RowVersion = rowVersion;
            this.LastTaskItemID = lastTaskItemID;
            this.DueDate = dueDate;
            this.SpentTimeMinutes = spendTimeMinutes;
            this.EffortEstimationMinutes = effortEstimationMinutes;
        }
    }
}
