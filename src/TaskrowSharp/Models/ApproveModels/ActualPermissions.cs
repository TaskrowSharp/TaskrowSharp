namespace TaskrowSharp.Models.ApproveModels;

public class ActualPermissions
{
    public bool CreateTask { get; set; }
    public bool CloseTask { get; set; }
    public bool ReopenTask { get; set; }
    public bool CreateSubtasks { get; set; }
    public bool CloseSubtasks { get; set; }
    public bool AppendTaskItem { get; set; }
    public bool ChangeTitle { get; set; }
    public bool ChangeDueDate { get; set; }
    public bool ChangeStatus { get; set; }
    public bool ChangeEffortEstimation { get; set; }
    public bool ChangeRequestContact { get; set; }
    public bool Extranet { get; set; }
    public bool CanForwardTask { get; set; }
    public bool CanAnswerTask { get; set; }
    public bool CanForwardToNextOwner { get; set; }
    public bool CanReplyToPrevOwner { get; set; }
    public bool CloseExtranet { get; set; }
    public bool ReopenExtranet { get; set; }
}
