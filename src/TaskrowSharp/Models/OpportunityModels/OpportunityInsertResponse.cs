namespace TaskrowSharp.Models.OpportunityModels;

public class OpportunityInsertResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public OpportunityEntity Entity { get; set; }
    public string TargetURL { get; set; }
}
