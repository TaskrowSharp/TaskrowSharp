namespace TaskrowSharp.Models.JobModels;

public class InsertOpportunityResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public OpportunityEntity Entity { get; set; }
    public string TargetURL { get; set; }

    public class OpportunityEntity
    {
        public Opportunity Opportunity { get; set; }
        public int EstimateCount { get; set; }
        public int EstimatePendingApprovalCount { get; set; }
        public int ProposalCount { get; set; }
    }
}
