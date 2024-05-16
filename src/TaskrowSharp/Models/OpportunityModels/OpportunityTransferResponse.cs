using TaskrowSharp.Models.JobModels;

namespace TaskrowSharp.Models.OpportunityModels;

public class OpportunityTransferToClientResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Opportunity Entity { get; set; }
    public string TargetURL { get; set; }
}
