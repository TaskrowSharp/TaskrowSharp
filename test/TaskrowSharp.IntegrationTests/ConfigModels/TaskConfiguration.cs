using System.Text.Json.Serialization;

namespace TaskrowSharp.IntegrationTests.TestModels
{
    internal class TaskConfiguration
    {
        [JsonPropertyName("taskUrl")]
        public string TaskUrl { get; set; }

        [JsonPropertyName("user1Email")]
        public string User1Email { get; set; }

        [JsonPropertyName("user2Email")]
        public string User2Email { get; set; }
    }
}