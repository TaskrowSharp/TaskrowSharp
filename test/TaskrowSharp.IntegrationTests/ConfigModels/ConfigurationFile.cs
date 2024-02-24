using Newtonsoft.Json;
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

        [JsonPropertyName("clients")]
        public List<ClientConfigurationItem>? Clients { get; set; }

        [JsonPropertyName("userIDs")]
        public List<int> UserIDs { get; set; }

        [JsonPropertyName("insertInvoiceData")]
        public InsertInvoiceDataConfigurationItem InsertInvoiceData { get; set; }

        [JsonPropertyName("insertJobData")]
        public InsertJobDataConfigurationItem InsertJobData { get; set; }

        internal class ClientConfigurationItem
        {
            [JsonPropertyName("clientID")]
            public int ClientID { get; set; }

            [JsonPropertyName("clientNickName")]
            public string ClientNickName { get; set; }

            [JsonPropertyName("jobNumbers")]
            public List<int> JobNumbers { get; set; }
        }

        internal class InsertInvoiceDataConfigurationItem
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

        internal class InsertJobDataConfigurationItem
        {
            [JsonPropertyName("clientID")]
            public int ClientID { get; set; }

            [JsonPropertyName("clientNickName")]
            public string ClientNickName { get; set; }

            [JsonPropertyName("jobTitle")]
            public string JobTitle { get; set; }

            [JsonPropertyName("ownerUserID")]
            public int OwnerUserID { get; set; }

            [JsonPropertyName("jobTypeID")]
            public int JobTypeID { get; set; }

            [JsonPropertyName("requiredProduct")]
            public bool RequiredProduct { get; set; }

            [JsonPropertyName("public")]
            public bool Public { get; set; }

            [JsonPropertyName("externalUserAccess")]
            public bool ExternalUserAccess { get; set; }

            [JsonPropertyName("healthReference")]
            public int HealthReference { get; set; }

            [JsonPropertyName("isPrivate")]
            public bool IsPrivate { get; set; }

            [JsonPropertyName("effortRequired")]
            public bool EffortRequired { get; set; }

            [JsonPropertyName("looseEntriesAllowed")]
            public bool LooseEntriesAllowed { get; set; }

            [JsonPropertyName("deliverableRequired")]
            public bool DeliverableRequired { get; set; }

            [JsonPropertyName("requestDeliveryEnforceabilityID")]
            public int RequestDeliveryEnforceabilityID { get; set; }

            [JsonPropertyName("clientAreaID")]
            public int ClientAreaID { get; set; }

            [JsonPropertyName("jobSubTypeID")]
            public int JobSubTypeID { get; set; }
        }
    }
}