using System.Collections.Generic;
using TaskrowSharp.Models.BasicDataModels;
using TaskrowSharp.Models.ExternalModels;
using TaskrowSharp.Models.JobModels;
using TaskrowSharp.Models.TaskModels;

namespace TaskrowSharp.Models.ClientModels;

public class ClientDetailEntity
{
    public string DefaultCountryID { get; set; }
    public List<CountryList> CountryList { get; set; } = [];
    public List<string> StateList { get; set; } = [];
    public List<SegmentList> SegmentList { get; set; } = [];
    public List<ProductList> ProductList { get; set; } = [];
    public Client Client { get; set; }
    public List<ExtranetPipeline> ExtranetPipelines { get; set; } = [];
    public List<JobPipeline> JobPipelines { get; set; } = [];
    public List<Pipeline> Pipelines { get; set; } = [];
    public List<ContractList> ContractList { get; set; } = [];
}
