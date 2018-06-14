using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class TasksByGroupEntityApi
    {
        public List<TaskHeaderApi> ClosedTasks { get; set; }
        public List<TaskHeaderApi> OpenTasks { get; set; }
        public List<TaskHeaderApi> OpenTasksDelayed { get; set; }
        public List<TaskHeaderApi> OpenTasksToNextWeek { get; set; }

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
