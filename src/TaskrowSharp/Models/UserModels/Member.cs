﻿using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.UserModels;

public class Member
{
    public string UserLogin { get; set; }
    public int UserID { get; set; }
    public string UserHashCode { get; set; }
    public bool Creator { get; set; }
    //public object? Notify { get; set; }
    public bool? MustRead { get; set; }
    public bool? Read { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? LastReadDate { get; set; }

    //public object? Favorite { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? LastModificationDate { get; set; }

    public User User { get; set; }
    public bool Passed { get; set; }
    public int Order { get; set; }
    public string FullName { get; set; }
    public string ExternalCode { get; set; }
    public int PhotoVersion { get; set; }
    public string ApprovalGroup { get; set; }
}
