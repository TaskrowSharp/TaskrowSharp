using System.Text.Json.Serialization;
using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.JobModels;

public class JobInfo
{
    [JsonPropertyName("jobID")]
    public int JobID { get; set; }

    [JsonPropertyName("jobNumber")]
    public int JobNumber { get; set; }

    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; }

    [JsonPropertyName("inactive")]
    public bool Inactive { get; set; }

    [JsonPropertyName("externalCode")]
    public string ExternalCode { get; set; }

    [JsonPropertyName("client")]
    public ClientReference Client { get; set; }

    [JsonPropertyName("companyAddressID")]
    public int? CompanyAddressID { get; set; }
}
