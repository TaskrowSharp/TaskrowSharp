using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.IntegrationTests
{
    public class ConfigurationFile
    {
        [JsonPropertyName("serviceUrl")]
        public string ServiceUrl { get; set; }

        [JsonPropertyName("accessKey")]
        public string AccessKey { get; set; }

        [JsonPropertyName("forwardTaskTests")]
        public List<ForwardTaskConfiguration> ForwardTaskTests { get; set; }
    }
}
