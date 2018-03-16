using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class TasksByGroupEntityApi
    {
        public List<TaskApi> ClosedTasks { get; set; }
        public List<TaskApi> OpenTasks { get; set; }
        public List<TaskApi> OpenTasksDelayed { get; set; }
        public List<TaskApi> OpenTasksToNextWeek { get; set; }

        //public List<object> HierarchyGroups { get; set; }
        public int UserGroupID { get; set; }
        public bool ViewPanelReportPermission { get; set; }
        //public CurrentUser CurrentUser { get; set; }
        //public List<object> Jobs { get; set; }
        //public List<object> FunctionGroupJobs { get; set; }
        //public List<object> Clients { get; set; }
        public int Context { get; set; }
        public int SelectedGroupID { get; set; }
        //public List<object> Calendar { get; set; }
    }
}
