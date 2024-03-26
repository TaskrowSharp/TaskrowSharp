using System.Collections.Generic;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.JobModels;

public class Job
{
    public int JobID { get; set; }
    public int JobNumber { get; set; }
    public string JobTitle { get; set; }
    public string JobDisplayTitle { get; set; }
    public int JobTypeID { get; set; }
    public int ClientID { get; set; }
    public ClientReference Client { get; set; }
    public string TagListString { get; set; }
    public string MemberListString { get; set; }
    public string MemberInfoListString { get; set; }
    public string TypeName { get; set; }
    public int JobStatusID { get; set; }
    public int PipelineID { get; set; }
    public string CreationDate { get; set; }
    public UserReference CreationUser { get; set; }
    public string LastModificationDate { get; set; }
    public UserReference LastModificationUser { get; set; }
    public object ContractID { get; set; }
    public bool JobWithContract { get; set; }
    public string JobStatus { get; set; }
    public string UrlData { get; set; }
    public string EditUrlData { get; set; }
    public int OwnerUserID { get; set; }
    public string OwnerUserLogin { get; set; }
    public string OwnerUserHashCode { get; set; }
    public object DaysToAnswerRequest { get; set; }
    public List<object> Tags { get; set; }
    public List<JobMember> JobMember { get; set; }
    public JobDetail JobDetail { get; set; }
    public object InternalNumber { get; set; }
    public object ExternalCode { get; set; }
    public bool IsPrivate { get; set; }
    public object MonthStart { get; set; }
    public object MonthEnd { get; set; }
    public object HealthOftenRequired { get; set; }
    public object HealthReference { get; set; }
    public object JobHealth { get; set; }
    public object ExtranetUserID { get; set; }
    public object DaysToExternalRequests { get; set; }
    public string JobExtranetMemberListString { get; set; }
    public string JobExtranetMemberInfoListString { get; set; }
    public object JobExtranetUser { get; set; }
    public List<object> JobExtranetMember { get; set; }
    public bool ExternalRequestsEnabled { get; set; }
    public bool ExtranetShowSubtasks { get; set; }
    public List<object> JobExternalMembers { get; set; }
    public object TagContextID { get; set; }
    public string TagContext { get; set; }
    public bool EffortRequired { get; set; }
    public bool TemplateRequired { get; set; }
    public bool LooseEntriesAllowed { get; set; }
    public List<object> DeliverableList { get; set; }
    public object ProductID { get; set; }
    public object Product { get; set; }
    public bool DeliverableRequired { get; set; }
    public bool Public { get; set; }
    public bool ExternalUserAccess { get; set; }
    public object JobTypeModificationDate { get; set; }
    public object JobTypeModificationUser { get; set; }
    public int JobPipelineStepID { get; set; }
    public JobPipelineStep JobPipelineStep { get; set; }
    public int ClientAreaID { get; set; }
    public object ClientArea { get; set; }
    public int JobSubTypeID { get; set; }
    public object JobSubType { get; set; }
    public int RequestDeliveryEnforceabilityID { get; set; }
    public object CompanyAddressID { get; set; }
}
