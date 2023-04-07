using System.Text.Json.Serialization;

namespace TaskrowSharp.Models
{
    public class UpdateInvoiceResponse
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        [JsonPropertyName("Entity")]
        public Invoice? Entity { get; set; }

        public string? TargetURL { get; set; }
    }
}
