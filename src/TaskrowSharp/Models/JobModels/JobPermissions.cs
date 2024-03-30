namespace TaskrowSharp.Models.JobModels;

public class JobPermissions
{
    public bool CreateTask { get; set; }
    public bool CreateJobs { get; set; }
    public bool ViewHoursAndExpensesReport { get; set; }
    public bool ManagementJob { get; set; }
    public bool ApproveJob { get; set; }
    public bool AllowClosingDate { get; set; }
    public bool ViewJobAttachments { get; set; }
    public bool ADMCompanySettings { get; set; }
}
