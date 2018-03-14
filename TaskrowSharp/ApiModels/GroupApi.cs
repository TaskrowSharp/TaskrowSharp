using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class GroupApi
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public object Slug { get; set; }
        public int OwnerUserID { get; set; }
        public OwnerApi Owner { get; set; }
        public List<MemberGroupApi> Members { get; set; }
        public List<object> AllowedUsers { get; set; }
        public string MembersNames { get; set; }
        public ParentGroupApi ParentGroup { get; set; }
        public List<object> Groups { get; set; }
    }
}
