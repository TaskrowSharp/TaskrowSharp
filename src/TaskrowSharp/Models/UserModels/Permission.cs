namespace TaskrowSharp.Models.UserModels;

public class Permission
{
    public int PermissionID { get; set; }
    public string PermissionTitle { get; set; }
    public bool HeaderMenu { get; set; }
    public string EntityType { get; set; }
    public string UrlAction { get; set; }
    public string MenuLabel { get; set; }
    public bool ApprovalRequired { get; set; }
    public int? ParentPermissionID { get; set; }
    public bool ExternalProfileAllowed { get; set; }
}
