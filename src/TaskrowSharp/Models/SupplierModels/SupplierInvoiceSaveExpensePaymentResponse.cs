using TaskrowSharp.Models.InvoiceModels;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierInvoiceSaveExpensePaymentResponse
{
    public bool? Success { get; set; }
    public string Message { get; set; }
    public SupplierInvoiceExpensePayment? Entity { get; set; }
    public string? TargetURL { get; set; }
}
