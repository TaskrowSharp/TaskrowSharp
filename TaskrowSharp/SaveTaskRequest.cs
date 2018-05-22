using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public int SpentTime { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public int PercentComplete { get; set; }

        public int EffortEstimation { get; set; }

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
            int percentComplete,
            int effortEstimation)
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
            this.SpentTime = spendTimeMinutes;
            this.PercentComplete = percentComplete;
            this.EffortEstimation = effortEstimation;

            //"TaskID": 461373,
            //"TaskNumber": 1501,
            //"JobNumber": 488,
            //"ClientNickName": "Adidas",
            //"TaskTitle": "Teste encaminhar tarefa",
            //"RowVersion": "0b36b27b-ce1c-449f-afcf-329064effeb7",
            //"LastTaskItemID": 2414801,
            //"TaskItemComment": "Teste Postman 1",
            //"OwnerUserID": 15619,
            //"DueDate": "2017-12-29",
            //"SpentTime": 0,
            //"PercentComplete" : 0,
            //"EffortEstimation": 0,
            
            //"TagListString" : null
            //"EffortUnitListString" : null,
        }
    }
}
