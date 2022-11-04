namespace TaskrowSharp.Models
{
    public class UserRelationType
    {
        public int UserRelationTypeID { get; set; }
        public int AppMainCompanyID { get; set; }
        public string? Name { get; set; }
        //public object? DailyMinutes { get; set; }
        //public object? DailyMinutesLimit { get; set; }
        public bool Inactive { get; set; }
        public bool AllowSendIncompleteTimesheet { get; set; }
        public bool VacationPeriod { get; set; }
        public bool AccountsCompTime { get; set; }
        public bool? ExternalUser { get; set; }
        //public string? UserRelationType { get; set; }
    }
}
