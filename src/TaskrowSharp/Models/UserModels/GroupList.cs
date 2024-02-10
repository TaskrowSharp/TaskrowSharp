using TaskrowSharp.Models.TaskModels;

namespace TaskrowSharp.Models.UserModels;

public class GroupList
{
    public int GroupID { get; set; }
    public string GroupHierarchy { get; set; }
    public string GroupName { get; set; }
    public int GroupTypeID { get; set; }
    public string GroupType { get; set; }
    public int OwnerUserID { get; set; }
    public Owner Owner { get; set; }
}
