﻿using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceBillSaveRequest
{
    public int InvoiceID { get; set; }
    public int InvoiceBillID { get; set; }
    public string GuidModification { get; set; }
    public int BillTypeID { get; set; }
    public decimal BillValue { get; set; }
    public int FinancialAccountID { get; set; }
    public int ClientAddressID { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DueDate { get; set; }

    public string Memo { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? MemoDueDate { get; set; }

    public InvoiceBillSaveRequest(int invoiceID, int billTypeID, decimal billValue, int financialAccountID,
        int clientAddressID, DateTime? dueDate)
    {
        InvoiceID = invoiceID;
        BillTypeID = billTypeID;
        BillValue = billValue;
        FinancialAccountID = financialAccountID;
        ClientAddressID = clientAddressID;
        DueDate = dueDate;
    }
}
