using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierOrder
{
    [JsonPropertyName("supplierOrderID")]
    public int SupplierOrderID { get; set; }

    [JsonPropertyName("clientContactID")]
    public int ClientContactID { get; set; }

    [JsonPropertyName("clientAddressID")]
    public int ClientAddressID { get; set; }

    [JsonPropertyName("refundTypeID")]
    public int RefundTypeID { get; set; }

    [JsonPropertyName("supplierID")]
    public int SupplierID { get; set; }

    [JsonPropertyName("supplierDisplayName")]
    public string SupplierDisplayName { get; set; }

    [JsonPropertyName("jobID")]
    public int JobID { get; set; }

    [JsonPropertyName("jobNumber")]
    public int JobNumber { get; set; }

    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; }

    [JsonPropertyName("jobDisplayTitle")]
    public string JobDisplayTitle { get; set; }

    [JsonPropertyName("clientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("clientDisplayName")]
    public string ClientDisplayName { get; set; }

    [JsonPropertyName("clientNickName")]
    public string ClientNickName { get; set; }

    [JsonPropertyName("cost")]
    public double Cost { get; set; }

    [JsonPropertyName("realCost")]
    public double RealCost { get; set; }

    [JsonPropertyName("internalDescription")]
    public string InternalDescription { get; set; }

    [JsonPropertyName("supplierDescription")]
    public string SupplierDescription { get; set; }

    [JsonPropertyName("supplierOrderNumber")]
    public int SupplierOrderNumber { get; set; }

    [JsonPropertyName("userModification")]
    public int UserModification { get; set; }

    [JsonPropertyName("userLoginModification")]
    public string UserLoginModification { get; set; }

    [JsonPropertyName("guidModification")]
    public string GuidModification { get; set; }

    [JsonPropertyName("dateModification")]
    public DateTime DateModification { get; set; }

    [JsonPropertyName("statusID")]
    public int StatusID { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("sentDate")]
    public object SentDate { get; set; }

    [JsonPropertyName("isCanceled")]
    public bool IsCanceled { get; set; }

    [JsonPropertyName("cashback")]
    public double Cashback { get; set; }

    [JsonPropertyName("incentiveValue")]
    public double IncentiveValue { get; set; }

    [JsonPropertyName("supplierOrderItem")]
    public List<SupplierOrderItem> SupplierOrderItem { get; set; }

    [JsonPropertyName("installment")]
    public List<Installment> Installments { get; set; }

    [JsonPropertyName("supplierOrderApprovals")]
    public List<object> SupplierOrderApprovals { get; set; }

    [JsonPropertyName("pendingApprovals")]
    public List<object> PendingApprovals { get; set; }

    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; set; }

    [JsonPropertyName("generalClauses")]
    public List<object> GeneralClauses { get; set; }

    [JsonPropertyName("supplierAddress")]
    public SupplierAddress SupplierAddress { get; set; }

    [JsonPropertyName("supplierRevenueTypeID")]
    public int SupplierRevenueTypeID { get; set; }

    [JsonPropertyName("letterOfAgreementCreated")]
    public bool LetterOfAgreementCreated { get; set; }

    [JsonPropertyName("letterOfAgreementNumber")]
    public int LetterOfAgreementNumber { get; set; }

    [JsonPropertyName("detailedLetterOfAgreement")]
    public bool DetailedLetterOfAgreement { get; set; }
}
