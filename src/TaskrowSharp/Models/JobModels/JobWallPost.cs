using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.JobModels;

public class JobWallPost
{
    public int WallPostID { get; set; }
    public int WallID { get; set; }
    public string CreationDate { get; set; }
    public string Text { get; set; }
    public bool IsPublic { get; set; }
    public bool Inactive { get; set; }
    public int CreationUserID { get; set; }
    public int WallPostTypeID { get; set; }
    public string WallPostType { get; set; }
    public Wall Wall { get; set; }
    //public List<object> WallComment { get; set; } = [];
    public UserReference CreationUser { get; set; }
    //public List<object> Attachments { get; set; } = [];
}
