namespace TaskrowSharp.Models
{
    public class TaskFromGroup
    {
        public int AppMainCompanyID { get; set; }
        public int TaskNumber { get; set; }
        public int TaskID { get; set; }
        public string? RowVersion { get; set; }
        public string? TaskTitle { get; set; }
        public int ParentTaskID { get; set; }
        public int? ParentTaskNumber { get; set; }
        public string? ParentTaskTitle { get; set; }
        public string? MainTaskTitle { get; set; }
        public string? PipelineStep { get; set; }
        //public object? ColorID { get; set; }
        public string? DueDate { get; set; }
        public int AbsoluteUserOrder { get; set; }
        public string? StandByDate { get; set; }
        public int EffortEstimation { get; set; }
        public int? RemainingEffortEstimation { get; set; }
        //public object? ClosingDate { get; set; }
        public bool Closed { get; set; }
        public string? UrlData { get; set; }
        public string? Tags { get; set; }
        public bool HasAttachment { get; set; }
        public bool HasExtranetItems { get; set; }
        public int OwnerUserID { get; set; }
        public string? OwnerUserLogin { get; set; }
        public string? OwnerUserHashCode { get; set; }
        public string? LastReadByOwnerDate { get; set; }
        public bool Read { get; set; }
        public int FunctionGroupID { get; set; }
        public string? FunctionGroupTitle { get; set; }
        public int UserFunctionID { get; set; }
        public string? UserFunctionTitle { get; set; }
        public string? RequestTypeAcronym { get; set; }
        public string? RequestTypeName { get; set; }
        //public object? RequestTypeColorID { get; set; }
        public int RequestTypeCount { get; set; }
        public string? RequestTypeModificationDate { get; set; }
        public int RequestTypeModificationUserID { get; set; }
        public string? RequestTypeModificationUserLogin { get; set; }
        public string? CreationDate { get; set; }
        public int CreationUserID { get; set; }
        public string? CreationUserLogin { get; set; }
        public int JobID { get; set; }
        public int JobNumber { get; set; }
        public string? JobTitle { get; set; }
        public string? JobDisplayTitle { get; set; }
        public string? JobUrlData { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public int ClientID { get; set; }
        public string? ClientNickName { get; set; }
        public string? ClientDisplayName { get; set; }
        public string? ClientUrlData { get; set; }
        public int ClientColorID { get; set; }
        public int? DeliverableID { get; set; }
        public string? DeliverableName { get; set; }
        public bool PrivateTask { get; set; }
        public bool JobIsPrivate { get; set; }
        public int ClosedSubtasks { get; set; }
        public int OpenSubtasks { get; set; }
        public int TotalSubtasks { get; set; }
    }
}
