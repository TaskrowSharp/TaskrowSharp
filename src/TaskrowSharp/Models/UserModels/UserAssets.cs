using System.Collections.Generic;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.JobModels;

namespace TaskrowSharp.Models.UserModels;

public class UserAssets
{
    public int UserID { get; set; }
    public int JobCount { get; set; }
    public List<JobList> JobList { get; set; } = [];
    public int ClientCount { get; set; }
    public List<ClientList> ClientList { get; set; } = [];
    public int GroupCount { get; set; }
    public List<GroupList> GroupList { get; set; } = [];
}
