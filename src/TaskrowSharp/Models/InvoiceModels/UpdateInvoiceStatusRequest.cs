using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.InvoiceModels;

public class UpdateInvoiceStatusRequest
{
    public int InvoiceID { get; set; }

    [JsonConverter(typeof(EnumJsonConverter<IntegrationStatusEnum>))]
    public IntegrationStatusEnum IntegrationStatusID { get; set; }

    public string Message { get; set; }

    public string GuidModification { get; set; }

    public UpdateInvoiceStatusRequest(int invoiceID, IntegrationStatusEnum integrationStatusID, string message, string guidModification)
    {
        InvoiceID = invoiceID;
        IntegrationStatusID = integrationStatusID;
        Message = message;
        GuidModification = guidModification;
    }
}
