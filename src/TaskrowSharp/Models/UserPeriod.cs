namespace TaskrowSharp.Models
{
    public class UserPeriod
    {
        public int UserID { get; set; }
        public int UserFunctionPeriodID { get; set; }
        public int UserFunctionID { get; set; }
        public string? UserFunctionTitle { get; set; }
        public string? FullFunctionName { get; set; }
        public int OfficeID { get; set; }
        public int UserRelationTypeID { get; set; }
        //public object? UserTerminationTypeID { get; set; }
        public string? UserTerminationType { get; set; }
        //public object? RegistrationNumber { get; set; }
        //public object? FunctionRate { get; set; }
        //public object? FunctionMargin { get; set; }
        //public object? NetValue { get; set; }
        //public object? GrossValue { get; set; }
        public int UserPeriodID { get; set; }
        public string? OfficeName { get; set; }
        public string? UserRelationTypeName { get; set; }
        public string? DateStart { get; set; }
        public int StartDayID { get; set; }
        public string? DateEnd { get; set; }
        public int? EndDayID { get; set; }
    }
}
