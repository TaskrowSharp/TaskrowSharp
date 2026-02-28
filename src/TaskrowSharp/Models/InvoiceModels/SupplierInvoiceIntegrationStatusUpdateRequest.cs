using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.InvoiceModels;

public class SupplierInvoiceIntegrationStatusUpdateRequest
{
    public int SupplierInvoiceID { get; set; }

    [JsonConverter(typeof(EnumJsonConverter<IntegrationStatusEnum>))]
    public IntegrationStatusEnum IntegrationStatusID { get; set; }

    //public string Message { get; set; }
    //public string GuidModification { get; set; }

    public SupplierInvoiceIntegrationStatusUpdateRequest(int supplierInvoiceID, IntegrationStatusEnum integrationStatusID)
    {
        SupplierInvoiceID = supplierInvoiceID;
        IntegrationStatusID = integrationStatusID;
        //Message = message;
        //GuidModification = guidModification;
    }
}
