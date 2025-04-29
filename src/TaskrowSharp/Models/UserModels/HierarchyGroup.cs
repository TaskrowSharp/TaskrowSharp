using System.Collections.Generic;

namespace TaskrowSharp.Models.UserModels;

public class HierarchyGroup
{
    public int GroupID { get; set; }
    public string GroupName { get; set; }
    //public object? Slug { get; set; }
    public int OwnerUserID { get; set; }
    public List<Member> Members { get; set; } = [];
    public List<UserGroup> Groups { get; set; } = [];
}
