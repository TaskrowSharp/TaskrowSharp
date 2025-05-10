using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierInvoice
{
    public int SupplierInvoiceID { get; set; }
    public string InvoiceNumber { get; set; }
    public decimal InvoiceValue { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? IssueDate { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DueDate { get; set; }

    public bool EnforceNumberUniqueness { get; set; }
    public int ClientAddressID { get; set; }
    public ClientAddress ClientAddress { get; set; }
    public int SupplierInvoiceTypeID { get; set; }
    public SupplierInvoiceType? SupplierInvoiceType { get; set; }
    public int SupplierID { get; set; }
    public Supplier? Supplier { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? ModificationDate { get; set; }

    public int? ModificationUserID { get; set; }
    public UserReference? ModificationUser { get; set; }
    
    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? CreationDate { get; set; }

    public int? CreationUserID { get; set; }
    public UserReference? CreationUser { get; set; }

    public int? IntegrationStatusID { get; set; }
    public string IntegrationStatus { get; set; }

    public int? DeletionStatusID { get; set; }
    public string DeletionStatus { get; set; }

    public int? RefundTypeID { get; set; }
    public List<Expense>? Expenses { get; set; } = [];

    //public List<object>? Attachments { get; set; } = [];
    
    public int? CostCenterID { get; set; }
    //public CostCenter? CostCenter { get; set; }

    public int? FromClientAddressID { get; set; }
    public FromClientAddress FromClientAddress { get; set; }

    public string Description { get; set; }

    public int? SourceClientAddressID { get; set; }
    public SupplierAddress? SourceClientAddress { get; set; }
}
