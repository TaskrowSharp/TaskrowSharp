using System.Text.Json.Serialization;
using TaskrowSharp.Models.ProductionModels;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierOrderItem
{
    [JsonPropertyName("supplierOrderItemID")]
    public int SupplierOrderItemID { get; set; }

    [JsonPropertyName("supplierOrderID")]
    public int SupplierOrderID { get; set; }

    [JsonPropertyName("budgetItemID")]
    public int BudgetItemID { get; set; }

    [JsonPropertyName("productionItemID")]
    public int ProductionItemID { get; set; }

    [JsonPropertyName("cost")]
    public decimal Cost { get; set; }

    [JsonPropertyName("realCost")]
    public decimal RealCost { get; set; }

    [JsonPropertyName("supplierTax")]
    public decimal SupplierTax { get; set; }

    [JsonPropertyName("cashback")]
    public decimal Cashback { get; set; }

    [JsonPropertyName("incentive")]
    public decimal Incentive { get; set; }

    [JsonPropertyName("incentiveValue")]
    public decimal IncentiveValue { get; set; }

    [JsonPropertyName("extraRealCost")]
    public decimal ExtraRealCost { get; set; }

    [JsonPropertyName("extraRealCostDescription")]
    public string ExtraRealCostDescription { get; set; }

    [JsonPropertyName("productionItem")]
    public ProductionItem ProductionItem { get; set; }
}
