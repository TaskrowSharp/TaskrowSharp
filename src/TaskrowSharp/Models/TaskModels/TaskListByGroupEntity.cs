using System.Collections.Generic;

namespace TaskrowSharp.Models.TaskModels;

public class TaskListByGroupEntity
{
    public List<TaskFromGroup> ClosedTasks { get; set; }
    public List<TaskFromGroup> OpenTasks { get; set; }
    public List<TaskFromGroup> OpenTasksDelayed { get; set; }
    public List<TaskFromGroup> OpenTasksToNextWeek { get; set; }
    //public List<object>? HierarchyGroups { get; set; }
    public int UserGroupID { get; set; }
    public bool ViewPanelReportPermission { get; set; }
    //public CurrentUser CurrentUser { get; set; }
    //public List<object> Jobs { get; set; }
    //public List<object> FunctionGroupJobs { get; set; }
    //public List<object> Clients { get; set; }
    public int Context { get; set; }
    public int SelectedGroupID { get; set; }
    //public List<object>? Calendar { get; set; }
    //public object? FixedJob { get; set; }
}
