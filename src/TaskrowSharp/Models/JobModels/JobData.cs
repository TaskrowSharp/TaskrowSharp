using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.TaskModels;

namespace TaskrowSharp.Models.JobModels;

public class JobData
{
    public int JobID { get; set; }
    public int JobNumber { get; set; }
    public string JobTitle { get; set; }
    public string JobDisplayTitle { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? CreationDate { get; set; }

    public int CreationUserID { get; set; }
    public int JobTypeID { get; set; }
    public string JobType { get; set; }
    public int JobStatusID { get; set; }
    public string EditUrlData { get; set; }
    public string UrlData { get; set; }
    //public object? DaysToAnswerRequest { get; set; }
    //public object? InternalNumber { get; set; }
    public bool IsPrivate { get; set; }
    public Owner Owner { get; set; }
    public Client Client { get; set; }
    //public object? MonthStart { get; set; }
    //public object? MonthEnd { get; set; }
    //public object? HealthOftenRequired { get; set; }
    //public object? HealthReference { get; set; }
    public int? ExtranetUserID { get; set; }
    public int? DaysToExternalRequests { get; set; }
    public int? TagContextID { get; set; }
    public bool EffortRequired { get; set; }
    public bool TemplateRequired { get; set; }
    public bool LooseEntriesAllowed { get; set; }
    public int? ProductID { get; set; }
    public Product Product { get; set; }
    public int RequestDeliveryEnforceabilityID { get; set; }
}
