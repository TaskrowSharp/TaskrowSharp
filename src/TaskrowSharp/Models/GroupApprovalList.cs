using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class GroupApprovalList
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupHierarchy { get; set; }
        public int GroupTypeID { get; set; }
        //public object? Slug { get; set; }
        public int OwnerUserID { get; set; }
        //public object? ParentGroupID { get; set; }
        public Owner? Owner { get; set; }
        public List<Member>? Members { get; set; }
        //public List<object>? Leaders { get; set; }
        //public List<object>? AllowedUsers { get; set; }
        public string? MembersNames { get; set; }
        public ParentGroup? ParentGroup { get; set; }
        public List<Group>? Groups { get; set; }
    }
}
