﻿using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceBillDueDate
{
    public int InvoiceBillDueDateID { get; set; }

    public int InvoiceBillID { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DueDate { get; set; }

    public decimal? BillValue { get; set; }

    public string MemoDueDate { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DateCreation { get; set; }

    public UserReference UserCreation { get; set; }
}
