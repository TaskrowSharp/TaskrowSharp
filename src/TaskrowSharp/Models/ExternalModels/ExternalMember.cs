using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.ExternalModels;

public class ExternalMember
{
    public int TaskExternalMemberID { get; set; }
    public bool Read { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? LastReadDate { get; set; }

    public Contact Contact { get; set; }
}
