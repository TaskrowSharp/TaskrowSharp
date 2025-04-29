using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using System.Collections.Generic;
using TaskrowSharp.Models.ExternalDataModels;
using TaskrowSharp.Models.TaskModels;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.UserModels;
using TaskrowSharp.Models.ApproveModels;

namespace TaskrowSharp.Models.IndexDataModels;

public class IndexData
{
    public ExternalServices ExternalServices { get; set; }
    //public List<object> CurrentUserExternalServices { get; set; } = [];
    public RegionalSettings RegionalSettings { get; set; }
    public CompanyRegionalSettings CompanyRegionalSettings { get; set; }
    public bool TimesheetLocked { get; set; }
    public bool PitstopRequired { get; set; }
    public List<int> CompanyPermissions { get; set; } = [];
    public List<int> ClientPermissions { get; set; } = [];
    public List<int> JobPermissions { get; set; } = [];
    public UserData UserData { get; set; }
    public InternalClient InternalClient { get; set; }
    public bool HasJobInternalSupport { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? CompanyDate { get; set; }

    public List<ApprovalGroup> ApprovalGroups { get; set; } = [];
    public UserAssets UserAssets { get; set; }
    //public List<object> UnreadMessages { get; set; } = [];
    public IndexDataPermissions Permissions { get; set; }
    public User User { get; set; }
    public Photos Photos { get; set; }
    public bool LockAbsentUser { get; set; }
    public List<EffortUnitList> EffortUnitList { get; set; } = [];
    public List<TagContextList> TagContextList { get; set; } = [];
    public bool IntranetContentEnabled { get; set; }
    public int? CountUnreadTasks { get; set; }
    public bool CurrentUserBillable { get; set; }
    public bool ShowTimesheet { get; set; }
    public int? CountNewIntranetContent { get; set; }
    public List<string> AllowerdAttachmentsExtensions { get; set; } = [];
    public bool ExpenseEntryEnabled { get; set; }
    public string GoogleBrowserApiKey { get; set; }
    public bool CompTimeEnabled { get; set; }
}
