using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TaskrowSharp.Models
{
    public class ApprovalGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string? GroupHierarchy { get; set; }
        public int? GroupTypeID { get; set; }
        public string? Slug { get; set; }
        public int? OwnerUserID { get; set; }
        public int? ParentGroupID { get; set; }
        public Owner? Owner { get; set; }
        //public object? Members { get; set; }
        //public object? Leaders { get; set; }
        //public object? AllowedUsers { get; set; }
        public string MembersNames { get; set; }
        public ParentGroup? ParentGroup { get; set; }
        public List<Group> Groups { get; set; }
    }
}
