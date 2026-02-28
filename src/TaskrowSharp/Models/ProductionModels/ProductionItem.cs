using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.ProductionModels;

public class ProductionItem
{
    [JsonPropertyName("appMainCompanyID")]
    public int AppMainCompanyID { get; set; }

    [JsonPropertyName("productionItemID")]
    public int ProductionItemID { get; set; }

    [JsonPropertyName("productionItemNumber")]
    public int ProductionItemNumber { get; set; }

    [JsonPropertyName("number")]
    public string Number { get; set; }

    [JsonPropertyName("expenseItemTypeID")]
    public int ExpenseItemTypeID { get; set; }

    [JsonPropertyName("expenseTypeID")]
    public int ExpenseTypeID { get; set; }

    [JsonPropertyName("expenseItemTypeName")]
    public string ExpenseItemTypeName { get; set; }

    [JsonPropertyName("expenseTypeName")]
    public string ExpenseTypeName { get; set; }

    [JsonPropertyName("expenseItemTypeName_ExpenseTypeName")]
    public object ExpenseItemTypeNameExpenseTypeName { get; set; }

    [JsonPropertyName("internalDescription")]
    public object InternalDescription { get; set; }

    [JsonPropertyName("supplierDescription")]
    public object SupplierDescription { get; set; }

    [JsonPropertyName("totalCost")]
    public decimal TotalCost { get; set; }

    [JsonPropertyName("totalCostTax")]
    public decimal TotalCostTax { get; set; }

    [JsonPropertyName("totalRealCost")]
    public decimal TotalRealCost { get; set; }

    [JsonPropertyName("comissionValue")]
    public decimal ComissionValue { get; set; }

    [JsonPropertyName("comissionTax")]
    public decimal ComissionTax { get; set; }

    [JsonPropertyName("costBalance")]
    public decimal CostBalance { get; set; }

    [JsonPropertyName("realCostBalance")]
    public decimal RealCostBalance { get; set; }

    [JsonPropertyName("extraRealCost")]
    public decimal ExtraRealCost { get; set; }

    [JsonPropertyName("supplierOrderAmount")]
    public decimal SupplierOrderAmount { get; set; }

    [JsonPropertyName("supplierOrderStatusID")]
    public int SupplierOrderStatusID { get; set; }

    [JsonPropertyName("supplierOrderStatus")]
    public string SupplierOrderStatus { get; set; }

    [JsonPropertyName("deliverableID")]
    public int DeliverableID { get; set; }

    [JsonPropertyName("deliverableName")]
    public object DeliverableName { get; set; }

    [JsonPropertyName("budgetID")]
    public int BudgetID { get; set; }

    [JsonPropertyName("budgetNumber")]
    public int BudgetNumber { get; set; }

    [JsonPropertyName("budgetTitle")]
    public string BudgetTitle { get; set; }

    [JsonPropertyName("budgetDisplayTitle")]
    public string BudgetDisplayTitle { get; set; }

    [JsonPropertyName("refundTypeID")]
    public int RefundTypeID { get; set; }

    [JsonPropertyName("jobID")]
    public int JobID { get; set; }

    [JsonPropertyName("jobDisplayTitle")]
    public string JobDisplayTitle { get; set; }

    [JsonPropertyName("supplierTax")]
    public decimal SupplierTax { get; set; }

    [JsonPropertyName("incentive")]
    public decimal Incentive { get; set; }

    [JsonPropertyName("clientAddressID")]
    public object ClientAddressID { get; set; }

    [JsonPropertyName("clientAddress")]
    public object ClientAddress { get; set; }

    [JsonPropertyName("supplierInvoiceValue")]
    public decimal SupplierInvoiceValue { get; set; }

    [JsonPropertyName("productionAuthorizedCost")]
    public decimal ProductionAuthorizedCost { get; set; }

    [JsonPropertyName("productionAuthorizedComissionValue")]
    public decimal ProductionAuthorizedComissionValue { get; set; }

    [JsonPropertyName("productionAuthorizedComissionTax")]
    public decimal ProductionAuthorizedComissionTax { get; set; }

    [JsonPropertyName("productionAuthorizedCostTax")]
    public decimal ProductionAuthorizedCostTax { get; set; }

    [JsonPropertyName("invoiceAuthorizedCost")]
    public decimal InvoiceAuthorizedCost { get; set; }

    [JsonPropertyName("invoiceAuthorizedComissionValue")]
    public decimal InvoiceAuthorizedComissionValue { get; set; }

    [JsonPropertyName("invoiceAuthorizedComissionTax")]
    public decimal InvoiceAuthorizedComissionTax { get; set; }

    [JsonPropertyName("invoiceAuthorizedCostTax")]
    public decimal InvoiceAuthorizedCostTax { get; set; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }

    [JsonPropertyName("frequency")]
    public decimal Frequency { get; set; }

    [JsonPropertyName("unitCost")]
    public decimal UnitCost { get; set; }

    [JsonPropertyName("releasedCost")]
    public decimal ReleasedCost { get; set; }

    [JsonPropertyName("hasCostReview")]
    public bool HasCostReview { get; set; }

    [JsonPropertyName("hasCostBalanceReview")]
    public bool HasCostBalanceReview { get; set; }

    [JsonPropertyName("pendingUpdateCount")]
    public int PendingUpdateCount { get; set; }
}
