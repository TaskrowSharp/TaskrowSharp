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

        [JsonPropertyName("taskUrls")]
        public List<string> TaskUrls { get; set; }

        [JsonPropertyName("clientIDs")]
        public List<int> ClientIDs { get; set; }

        [JsonPropertyName("userIDs")]
        public List<int> UserIDs { get; set; }

        [JsonPropertyName("insertInvoiceData")]
        public InsertInvoiceDataType InsertInvoiceData { get; set; }

        [JsonPropertyName("invoiceFees")]
        public List<InvoiceFee> InvoiceFees { get; set; }

        [JsonPropertyName("invoiceIDs")]
        public List<int> InvoiceIDs { get; set; }

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

        internal class InvoiceFee
        {
            [JsonPropertyName("jobNumber")]
            public int JobNumber { get; set; }

            [JsonPropertyName("invoiceFeeID")]
            public int InvoiceFeeID { get; set; }
        }
    }
}