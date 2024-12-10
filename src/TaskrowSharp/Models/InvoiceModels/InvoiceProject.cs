using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using TaskrowSharp.Models.JobModels;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceProject : IInvoiceFeeOrProject
{
    public int InvoiceProjectID { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? IADueDate { get; set; }

    public decimal IAValue { get; set; }

    public string DateReference { get; set; }

    public string IAMemo { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DateCreation { get; set; }

    public string IAAdmMemo { get; set; }

    public int IAStatusID { get; set; }

    public string IAStatus { get; set; }

    public UserReference UserModification { get; set; }

    public bool EditAllowed { get; set; }

    public bool EditDescriptionAllowed { get; set; }

    public JobReference Job { get; set; }

    //public object IABudgetItensValue { get; set; }

    public decimal IAComissionValue { get; set; }

    //public int InvoiceSupplierID { get; set; }

    public int InvoiceID { get; set; }

    public string InvoiceNumber { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? InvoiceIssueDate { get; set; }

    public bool InvoiceIsCancelled { get; set; }

    public bool InvoiceIsDeleted { get; set; }

    public decimal InvoiceGrossValue { get; set; }

    public decimal InvoiceDeductionValue { get; set; }

    public List<InvoiceBill> InvoiceBill { get; set; }

    public int JobClientAddressID { get; set; }

    //public object Expenses { get; set; }
}
