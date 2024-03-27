using System.Collections.Generic;

namespace TaskrowSharp.Models.JobModels;

public class JobClientDependeciesEntity
{
    //public List<object> Tags { get; set; }
    //public List<object> Products { get; set; }
    public List<ClientArea> ClientAreas { get; set; }
    //public List<object> ContractList { get; set; }
    public bool ClientInactive { get; set; }
    public bool JobRequiredProduct { get; set; }
    public int? DefaultPipelineID { get; set; }
    public int? DefaultContractID { get; set; }
}
