using System.Collections.Generic;

namespace TaskrowSharp.Models.TaskModels;

public class Pipeline
{
    public int PipelineID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CompanyDefault { get; set; }
    public bool ResetOnRequestTypeChange { get; set; }
    public bool Extranet { get; set; }
    public bool Inactive { get; set; }
    public List<PipelineStep> PipelineSteps { get; set; }
    //public List<object>? PipelineTrigger { get; set; }
}
