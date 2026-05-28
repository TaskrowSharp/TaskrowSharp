using System.Collections.Generic;

namespace TaskrowSharp.Models.FinanceiroModels;

public class PaymentCondition
{
    public string Name { get; set; }
    public bool Inactive { get; set; }
    public bool ManualEntry { get; set; }
    public List<PaymentConditionInstallment> Installments { get; set; }
}
