using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.IntegrationTests.TestModels
{
    internal class ConfigurationFile
    {
        [JsonPropertyName("serviceUrl")]
        public string? ServiceUrl { get; set; }

        [JsonPropertyName("accessKey")]
        public string? AccessKey { get; set; }

        [JsonPropertyName("tasks")]
        public List<TaskConfiguration> Tasks { get; set; }

        [JsonPropertyName("clients")]
        public List<int> Clients { get; set; }
    }
}