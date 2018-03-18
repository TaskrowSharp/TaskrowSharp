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
        
        public TaskDetail(int taskID, int taskNumber, string taskTitle, int jobID, string memberListString,
            List<TaskItem> taskItems, List<TaskTag> tags, string clientNickName, string rowVersion,
            DateTime dueDate, List<SubTask> subTasks)
        {
            this.TaskID = taskID;
            this.TaskNumber = taskNumber;
            this.TaskTitle = taskTitle;
            this.JobID = jobID;
            this.MemberListString = memberListString;
            this.TaskItems = taskItems;
            this.Tags = tags;
            this.ClientNickName = clientNickName;
            this.RowVersion = rowVersion;
            this.DueDate = dueDate;
            this.SubTasks = subTasks;
        }

        internal TaskDetail(ApiModels.TaskDetailResponseApi responseApi)
        {
            this.TaskID = responseApi.TaskData.TaskID;
            this.TaskNumber = responseApi.TaskData.TaskNumber;
            this.TaskTitle = responseApi.TaskData.TaskTitle;
            this.JobID = responseApi.TaskData.JobID;
            this.MemberListString = responseApi.TaskData.MemberListString;
            this.ClientNickName = responseApi.JobData.Client.ClientNickName;
            this.RowVersion = responseApi.TaskData.RowVersion;
            this.DueDate = Utils.Parser.ToDateTimeFromTaskrowDate(responseApi.TaskData.DueDate);
            
            //this.TaskItems = responseApi.TaskData.NewTaskItems;
            //this.Tags = responseApi.TaskData.TagListString;
            //this.SubTasks = ;
        }

        //private TaskDetail GetTaskDetailFromJson(Newtonsoft.Json.Linq.JToken taskData, Newtonsoft.Json.Linq.JToken jobData)
        //{
        //    var taskDetail = new TaskDetail();
        //
        //    taskDetail.TaskID = Utils.Parser.ToInt32(taskData["TaskID"]);
        //    taskDetail.TaskNumber = Utils.Parser.ToInt32(taskData["TaskNumber"]);
        //    taskDetail.TaskTitle = taskData["TaskTitle"].ToString();
        //    taskDetail.MemberListString = taskData["MemberListString"].ToString();
        //    taskDetail.RowVersion = taskData["RowVersion"].ToString();
        //    taskDetail.DueDate = Convert.ToDateTime(taskData["DueDate"].ToString());
        //
        //    taskDetail.TaskItems = new List<TaskItem>();
        //    foreach (var item in taskData["NewTaskItems"])
        //    {
        //        var taskItem = new TaskItem();
        //        taskItem.TaskItemID = Utils.Parser.ToInt32(item["TaskItemID"]);
        //        taskItem.OldOwnerUserID = Utils.Parser.ToInt32(item["OldOwnerUserID"]);
        //        taskItem.OldOwnerName = item["OldOwnerName"].ToString();
        //        taskItem.NewOwnerUserID = Utils.Parser.ToInt32(item["NewOwnerUserID"]);
        //        taskItem.NewOwnerName = item["NewOwnerName"].ToString();
        //        taskItem.TaskItemComment = item["TaskItemComment"].ToString();
        //        taskDetail.TaskItems.Add(taskItem);
        //    }
        //
        //    taskDetail.JobID = Utils.Parser.ToInt32(jobData["JobID"]);
        //    taskDetail.JobNumber = Utils.Parser.ToInt32(jobData["JobNumber"]);
        //    taskDetail.JobTitle = jobData["JobTitle"].ToString();
        //
        //    var clientData = jobData["Client"];
        //    taskDetail.ClientNickName = clientData["ClientNickName"].ToString();
        //
        //    taskDetail.Tags = new List<TaskTag>();
        //    foreach (var item in taskData["Tags"])
        //        taskDetail.Tags.Add(new TaskTag() { TaskTagID = Utils.Parser.ToInt32(item["TaskTagID"]), TagTitle = item["TagTitle"].ToString() });
        //
        //    taskDetail.SubTasks = new List<SubTask>();
        //    if (taskData["Subtasks"] != null)
        //    {
        //        foreach (var item in taskData["Subtasks"])
        //        {
        //            var subTask = new SubTask();
        //            subTask.SubtaskID = Utils.Parser.ToInt32(item["SubtaskID"]);
        //            subTask.TaskID = Utils.Parser.ToInt32(item["TaskID"]);
        //            subTask.ChildTaskID = Utils.Parser.ToInt32(item["ChildTaskID"]);
        //            subTask.Title = item["Title"].ToString();
        //
        //            var childTaskData = item["ChildTask"];
        //            if (childTaskData != null)
        //            {
        //                TaskDetail childTask = GetTaskDetailFromJson(childTaskData, jobData);
        //                subTask.ChildTask = childTask;
        //            }
        //
        //            taskDetail.SubTasks.Add(subTask);
        //        }
        //    }
        //
        //    return taskDetail;
        //}
    }
}
