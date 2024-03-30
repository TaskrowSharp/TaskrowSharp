using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.JobModels;

public class Wall
{
    public int WallID { get; set; }
    public int JobID { get; set; }
    public Job Job { get; set; }
    public int? ClientID { get; set; }
    public ClientReference? Client { get; set; }
}
