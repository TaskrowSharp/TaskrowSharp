namespace TaskrowSharp.ApiModels
{
    internal class UserFunctionApi
    {
        public int UserFunctionID { get; set; }
        public string UserFunctionTitle { get; set; }
        public int? DailyMinutes { get; set; }
        public bool NotBillable { get; set; }
        public bool NotRequiredTimesheet { get; set; }
        public int FunctionGroupID { get; set; }
        public string GroupName { get; set; }
        public string FullFunctionName { get; set; }
    }
}
