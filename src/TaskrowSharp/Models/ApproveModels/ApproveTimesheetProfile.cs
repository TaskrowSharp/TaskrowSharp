﻿namespace TaskrowSharp.Models.ApproveModels;

public class ApproveTimesheetProfile
{
    public int AppMainCompanyID { get; set; }
    public int ProfileID { get; set; }
    public int ProfileRate { get; set; }
    public string ProfileTitle { get; set; }
    public bool BuiltInUser { get; set; }
    public string PermissionListString { get; set; }
    public bool Inactive { get; set; }
    //public object? ExternalUser { get; set; }
    //public List<object> Permission { get; set; } = [];
    public bool HasMorePermissions { get; set; }
    //public List<object> Users { get; set; } = [];
}
