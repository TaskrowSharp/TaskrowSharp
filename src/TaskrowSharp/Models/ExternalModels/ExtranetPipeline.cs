using System.Collections.Generic;
using TaskrowSharp.Models.TaskModels;

namespace TaskrowSharp.Models.ExternalModels;

public class ExtranetPipeline
{
    public int PipelineID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CompanyDefault { get; set; }
    public bool ResetOnRequestTypeChange { get; set; }
    public bool Extranet { get; set; }
    public bool Inactive { get; set; }
    public List<PipelineStep> PipelineSteps { get; set; }
    public List<object> PipelineTrigger { get; set; }
}
