using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.InvoiceModels;

public class SupplierOrderIntegrationStatusUpdateRequest
{
    public int SupplierOrderID { get; set; }

    [JsonConverter(typeof(EnumIntValueJsonConverter<IntegrationStatusEnum>))]
    public IntegrationStatusEnum IntegrationStatusID { get; set; }

    public SupplierOrderIntegrationStatusUpdateRequest(int supplierOrderID, IntegrationStatusEnum integrationStatusID)
    {
        SupplierOrderID = supplierOrderID;
        IntegrationStatusID = integrationStatusID;
    }
}
