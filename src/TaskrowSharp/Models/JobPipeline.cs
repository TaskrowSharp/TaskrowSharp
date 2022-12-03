using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;
using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class JobPipeline
    {
        public int JobPipelineID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CompanyDefault { get; set; }
        public bool Extranet { get; set; }
        public bool Inactive { get; set; }
		
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? LastModificationDate { get; set; }
		
        public int LastModificationUserID { get; set; }
        public List<JobPipelineStep>? JobPipelineSteps { get; set; }
    }
}
