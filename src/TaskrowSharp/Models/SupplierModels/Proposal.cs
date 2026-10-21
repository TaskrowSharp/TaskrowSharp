using System.Text.Json.Serialization;
using TaskrowSharp.Models.JobModels;

namespace TaskrowSharp.Models.SupplierModels;

public class Proposal
{
    [JsonPropertyName("proposalID")]
    public int ProposalID { get; set; }

    [JsonPropertyName("proposalNumber")]
    public int ProposalNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("clientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("clientNumber")]
    public int ClientNumber { get; set; }

    [JsonPropertyName("opportunity")]
    public OpportunityReference Opportunity { get; set; }

    [JsonPropertyName("proposalDisplayName")]
    public string ProposalDisplayName { get; set; }
}
