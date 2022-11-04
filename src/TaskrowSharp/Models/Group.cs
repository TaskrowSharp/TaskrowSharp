using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupHierarchy { get; set; }
        public int GroupTypeID { get; set; }
        //public object? Slug { get; set; }
        //public object? OwnerUserID { get; set; }
        public int? ParentGroupID { get; set; }
        public Owner? Owner { get; set; }
        //public object? Members { get; set; }
        //public object? Leaders { get; set; }
        //public object? AllowedUsers { get; set; }
        public string? MembersNames { get; set; }
        public ParentGroup? ParentGroup { get; set; }
        //public List<object>? Groups { get; set; }
    }
}
