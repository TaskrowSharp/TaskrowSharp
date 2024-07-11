using TaskrowSharp.Models.JobModels;

namespace TaskrowSharp.Models.TaskModels;

public class JobUpdateRequest
{
    public int JobID { get; set; }
    public int JobNumber { get; set; }
    public string JobTitle { get; set; }
    public string JobDisplayTitle { get; set; }
    public int JobTypeID { get; set; }
    public int ClientID { get; set; }
    public string ClientNickName { get; set; }
    //public string TagListString { get; set; }
    //public string MemberListString { get; set; }
    //public string MemberInfoListString { get; set; }
    //public string TypeName { get; set; }
    public int JobStatusID { get; set; }
    public int PipelineID { get; set; }
    public int? ContractID { get; set; }
    public bool JobWithContract { get; set; }
    public string JobStatus { get; set; }
    public string UrlData { get; set; }
    public string EditUrlData { get; set; }
    public int OwnerUserID { get; set; }
    public string OwnerUserLogin { get; set; }
    public string OwnerUserHashCode { get; set; }
    //public object DaysToAnswerRequest { get; set; }
    //public List<object> Tags { get; set; }
    public string InternalNumber { get; set; }
    public string ExternalCode { get; set; }
    public bool IsPrivate { get; set; }
    //public object MonthStart { get; set; }
    //public object MonthEnd { get; set; }
    //public object HealthOftenRequired { get; set; }
    //public object HealthReference { get; set; }
    //public object JobHealth { get; set; }
    //public object ExtranetUserID { get; set; }
    //public object DaysToExternalRequests { get; set; }
    //public string JobExtranetMemberListString { get; set; }
    //public string JobExtranetMemberInfoListString { get; set; }
    public string JobExtranetUser { get; set; }
    //public List<object> JobExtranetMember { get; set; }
    public bool ExternalRequestsEnabled { get; set; }
    public bool ExtranetShowSubtasks { get; set; }
    //public List<object> JobExternalMembers { get; set; }
    public int? TagContextID { get; set; }
    public string? TagContext { get; set; }
    public bool EffortRequired { get; set; }
    public int EffortRequiredTypeID { get; set; }
    public bool TemplateRequired { get; set; }
    public bool LooseEntriesAllowed { get; set; }
    //public List<object> DeliverableList { get; set; }
    public int? ProductID { get; set; }
    //public object Product { get; set; }
    public bool DeliverableRequired { get; set; }
    public bool Public { get; set; }
    public bool ExternalUserAccess { get; set; }
    //public object JobTypeModificationDate { get; set; }
    //public object JobTypeModificationUser { get; set; }
    public int JobPipelineStepID { get; set; }
    public int? ClientAreaID { get; set; }
    //public object ClientArea { get; set; }
    public int? JobSubTypeID { get; set; }
    //public object JobSubType { get; set; }
    public int? RequestDeliveryEnforceabilityID { get; set; }
    public int? CompanyAddressID { get; set; }

    public JobUpdateRequest(int jobID, int jobNumber, string clientNickName)
    {
        this.JobID = jobID;
        this.JobNumber = jobNumber;
        this.ClientNickName = clientNickName;
    }

    public JobUpdateRequest(Job job)
    {
        this.JobID = job.JobID;
        this.JobNumber = job.JobNumber;
        this.JobTitle = job.JobTitle;
        this.JobDisplayTitle = job.JobDisplayTitle;
        this.JobTypeID = job.JobTypeID;
        this.ClientID = job.ClientID;
        this.ClientNickName = job.Client.ClientNickName;
        //this.TagListString
        //this.MemberListString
        //this.MemberInfoListString
        //this.TypeName
        this.JobStatusID = job.JobStatusID;
        this.PipelineID = job.PipelineID;
        this.ContractID = job.ContractID;
        this.JobWithContract = job.JobWithContract;
        //this.JobStatus = job.JobStatus/
        this.UrlData = job.UrlData;
        this.EditUrlData = job.EditUrlData;
        this.OwnerUserID = job.OwnerUserID;
        this.OwnerUserLogin = job.OwnerUserLogin;
        this.OwnerUserHashCode = job.OwnerUserHashCode;
        //this.DaysToAnswerRequest
        //this.Tags
        this.InternalNumber = job.InternalNumber;
        this.ExternalCode = job.ExternalCode;
        this.IsPrivate = job.IsPrivate;
        //this.MonthStart
        //this.MonthEnd
        //this.HealthOftenRequired
        //this.HealthReference
        //this.JobHealth
        //this.ExtranetUserID
        //this.DaysToExternalRequests
        //this.JobExtranetMemberListString = job.JobExtranetMemberListString;
        //this.JobExtranetMemberInfoListString = job.JobExtranetMemberInfoListString;
        this.JobExtranetUser = job.JobExtranetUser;
        //this.JobExtranetMember = job.JobExtranetMember;
        this.ExternalRequestsEnabled = job.ExternalRequestsEnabled;
        this.ExtranetShowSubtasks = job.ExtranetShowSubtasks;
        //this.JobExternalMembers = job.JobExternalMembers;
        this.TagContextID = job.TagContextID;
        this.TagContext = job.TagContext;
        this.EffortRequired = job.EffortRequired;
        //this.EffortRequiredTypeID = job.EffortRequiredTypeID;
        this.TemplateRequired = job.TemplateRequired;
        this.LooseEntriesAllowed = job.LooseEntriesAllowed;
        //this.DeliverableList = job.DeliverableList;
        this.ProductID = job.ProductID;
        //this.Product
        this.DeliverableRequired = job.DeliverableRequired;
        this.Public = job.Public;
        this.ExternalUserAccess = job.ExternalUserAccess;
        //this.JobTypeModificationDate
        //this.JobTypeModificationUser
        this.JobPipelineStepID = job.JobPipelineStepID;
        this.ClientAreaID = job.ClientAreaID;
        //this.ClientArea = job.ClientArea;
        this.JobSubTypeID = job.JobSubTypeID;
        //this.JobSubType = job.JobSubType
        this.RequestDeliveryEnforceabilityID = job.RequestDeliveryEnforceabilityID;
        this.CompanyAddressID = job.CompanyAddressID;
    }
}
