using TaskrowSharp.Models.JobModels;

namespace TaskrowSharp.Models.OpportunityModels;

public class OpportunityEntity
{
    public Opportunity Opportunity { get; set; }
    public int EstimateCount { get; set; }
    public int EstimatePendingApprovalCount { get; set; }
    public int ProposalCount { get; set; }
}
