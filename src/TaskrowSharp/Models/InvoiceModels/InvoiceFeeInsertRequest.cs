using System;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceFeeInsertRequest
{
    public int InvoiceFeeID { get; set; }
    public int FeeID { get; set; }
    public string GuidModification { get; set; }

    [JsonPropertyName("jobNumber")]
    public int JobNumber { get; private set; }

    public string FeePeriod { get; private set; }
    public string InvoiceForecast { get; private set; }

    [JsonPropertyName("FromClientAddressID")]
    public int FromClientAddressID { get; private set; }

    [JsonPropertyName("ClientAddressID")]
    public int ToClientAddressID { get; set; }

    public decimal MonthRevenueValue { get; set; }
    public decimal MonthExpectedValue { get; set; }
    public decimal InvoiceIAValue { get; set; }
    public decimal IAValue { get; set; }
    public DateTime IADueDate { get; set; }
    public int IAStatusID { get; set; }
    public int UserSigningDocument { get; set; }
    public string IAMemo { get; set; }
    public string IAAdmMemo { get; set; }

    public InvoiceFeeInsertRequest(int jobNumber, int feeMonth, int feeYear, int invoiceForecastMonth, int invoiceForecastYear,
        int fromClientAddressID, int toClientAddressID,
        decimal monthRevenueValue, decimal monthExpectedValue, decimal invoiceValue,
        DateTime dueDate,
        int invoiceStatusID, int userSigningDocument,
        string description,
        string administrativeNotes)
    {
        JobNumber = jobNumber;
        FeePeriod = new DateTime(feeYear, feeMonth, 1).ToString("MM/yyyy");
        InvoiceForecast = new DateTime(invoiceForecastYear, invoiceForecastMonth, 1).ToString("MM/yyyy");
        FromClientAddressID = fromClientAddressID;
        ToClientAddressID = toClientAddressID;
        MonthRevenueValue = monthRevenueValue;
        MonthExpectedValue = monthExpectedValue;
        InvoiceIAValue = invoiceValue;
        IAValue = invoiceValue;
        IADueDate = dueDate;
        IAStatusID = invoiceStatusID;
        UserSigningDocument = userSigningDocument;
        IAMemo = description;
        IAAdmMemo = administrativeNotes;
    }
}
