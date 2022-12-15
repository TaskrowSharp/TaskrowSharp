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
        public List<string> Tasks { get; set; }

        [JsonPropertyName("clients")]
        public List<int> Clients { get; set; }

        [JsonPropertyName("users")]
        public List<int> Users { get; set; }

        [JsonPropertyName("insertInvoiceData")]
        public InsertInvoiceDataType InsertInvoiceData { get; set; }

        internal class InsertInvoiceDataType
        {
            [JsonPropertyName("jobNumber")]
            public int JobNumber { get; set; }

            [JsonPropertyName("fromClientAddressID")]
            public int FromClientAddressID { get; set; }

            [JsonPropertyName("toClientAddressID")]
            public int ToClientAddressID { get; set; }

            [JsonPropertyName("userSigningDocument")]
            public int UserSigningDocument { get; set; }
        }
    }
}