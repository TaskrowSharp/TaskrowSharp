using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class UserAssets
    {
        public int UserID { get; set; }
        public int JobCount { get; set; }
        public List<JobList>? JobList { get; set; }
        public int ClientCount { get; set; }
        public List<ClientList>? ClientList { get; set; }
        public int GroupCount { get; set; }
        public List<GroupList>? GroupList { get; set; }
    }
}
