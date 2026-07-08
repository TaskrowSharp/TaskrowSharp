using TaskrowSharp.Models.InvoiceModels;

namespace TaskrowSharp.Models.SupplierModels;

public class ExpenseSaveExpensePaymentResponse
{
    public bool? Success { get; set; }
    public string Message { get; set; }
    public ExpensePayment? Entity { get; set; }
    public string? TargetURL { get; set; }
}
