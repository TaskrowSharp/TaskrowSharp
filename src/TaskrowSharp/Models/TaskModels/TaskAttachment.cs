namespace TaskrowSharp.Models.TaskModels;

public class TaskAttachment
{
    public int TaskID { get; set; }
    public int TaskItemID { get; set; }
    public string Identification { get; set; }
    public string Name { get; set; }
    public string MimeType { get; set; }
    public int TaskAttachmentID { get; set; }
    public int AttachmentTypeID { get; set; }
    public string CreationDate { get; set; }
    public int SizeInKB { get; set; }
    public int CreationUserID { get; set; }
    //public object? ApprovalRequestItem { get; set; }
    //public object? ApprovalRequest { get; set; }
}
