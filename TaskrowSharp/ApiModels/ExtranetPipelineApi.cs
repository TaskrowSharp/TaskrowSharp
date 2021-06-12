using System.Collections.Generic;

namespace TaskrowSharp.ApiModels
{
    internal class ExtranetPipelineApi
    {
        public int PipelineID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CompanyDefault { get; set; }
        public bool Extranet { get; set; }
        public bool ResetOnRequestTypeChange { get; set; }
        public List<PipelineStepApi> PipelineSteps { get; set; }
    }
}
