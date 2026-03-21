using System.Text.Json.Serialization;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.JobModels;

public class JobPipelineStepUpdateEntity
{
    [JsonPropertyName("JobHealthID")]
    public int? JobHealthID { get; set; }

    [JsonPropertyName("AppMainCompanyID")]
    public int? AppMainCompanyID { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("FormatDescription")]
    public string FormatDescription { get; set; }

    [JsonPropertyName("Percent")]
    public int? Percent { get; set; }

    [JsonPropertyName("HealthID")]
    public int? HealthID { get; set; }

    [JsonPropertyName("Health")]
    public string? Health { get; set; }

    [JsonPropertyName("CreationDate")]
    public string? CreationDate { get; set; }

    [JsonPropertyName("CreationUserID")]
    public int? CreationUserID { get; set; }

    [JsonPropertyName("CreationUser")]
    public UserReference? CreationUser { get; set; }

    [JsonPropertyName("JobID")]
    public int? JobID { get; set; }

    [JsonPropertyName("Job")]
    public JobReference? Job { get; set; }

    [JsonPropertyName("JobPipelineStepID")]
    public int? JobPipelineStepID { get; set; }

    [JsonPropertyName("JobPipelineStep")]
    public JobPipelineStep? JobPipelineStep { get; set; }

    //[JsonPropertyName("MonthEnd")]
    //public object MonthEnd { get; set; }
}
