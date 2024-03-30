using System.Collections.Generic;
using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.JobModels;

public class JobWall
{
    public int WallID { get; set; }
    public string CreationDate { get; set; }
    public int JobID { get; set; }
    public Job Job { get; set; }
    public int? ClientID { get; set; }
    public ClientReference? Client { get; set; }
    public int TotalWallPost { get; set; }
    public string LastUpdateTime { get; set; }
    public List<JobWallPost> WallPost { get; set; }
}
