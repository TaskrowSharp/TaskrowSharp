using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    public class SaveTaskRequestApi
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

        public int SpentTime { get; set; }

        public DateTime DueDate { get; set; }

        public int EffortEstimation { get; set; }

        public SaveTaskRequestApi(SaveTaskRequest request)
        {
            this.TaskID = request.TaskID;
            this.ClientNickname = request.ClientNickname;
            this.JobNumber = request.JobNumber;
            this.TaskNumber = request.TaskNumber;
            this.TaskTitle = request.TaskTitle;
            this.TaskItemComment = request.TaskItemComment;
            this.OwnerUserID = request.OwnerUserID;
            this.RowVersion = request.RowVersion;
            this.LastTaskItemID = request.LastTaskItemID;
            this.DueDate = request.DueDate;
            this.SpentTime = request.SpentTimeMinutes;
            this.EffortEstimation = request.EffortEstimationMinutes;
        }
    }
}
