using System.Text.Json.Serialization;

namespace TaskrowSharp.Models
{
    public class SaveInvoiceResponse
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        [JsonPropertyName("Entity")]
        public Invoice? Entity { get; set; }

        public string? TargetURL { get; set; }
    }
}
