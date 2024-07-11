using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceFeeInsertResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    [JsonPropertyName("Entity")]
    public List<InvoiceFee> Entities { get; set; }
    public string TargetURL { get; set; }
}
