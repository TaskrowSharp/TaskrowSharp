using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class ProfileList
    {
        public int AppMainCompanyID { get; set; }
        public int ProfileID { get; set; }
        public int ProfileRate { get; set; }
        public string ProfileTitle { get; set; }
        public bool BuiltInUser { get; set; }
        public string? PermissionListString { get; set; }
        public bool Inactive { get; set; }
        public bool? ExternalUser { get; set; }
        public List<Permission>? Permission { get; set; }
        public bool HasMorePermissions { get; set; }
        public List<User>? Users { get; set; }
    }
}
