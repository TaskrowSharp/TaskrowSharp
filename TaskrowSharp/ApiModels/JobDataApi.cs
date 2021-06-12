namespace TaskrowSharp.ApiModels
{
    internal class JobDataApi
    {
        public int JobID { get; set; }
        public int JobNumber { get; set; }
        public string JobTitle { get; set; }
        public string JobDisplayTitle { get; set; }
        public string CreationDate { get; set; }
        public int CreationUserID { get; set; }
        public int JobTypeID { get; set; }
        public string JobType { get; set; }
        public int JobStatusID { get; set; }
        public string EditUrlData { get; set; }
        public string UrlData { get; set; }
        public object DaysToAnswerRequest { get; set; }
        public object InternalNumber { get; set; }
        public bool IsPrivate { get; set; }
        public OwnerApi Owner { get; set; }
        public ClientApi Client { get; set; }
        //public object MonthStart { get; set; }
        //public object MonthEnd { get; set; }
        //public object HealthOftenRequired { get; set; }
        //public object HealthReference { get; set; }
        //public object ExtranetUserID { get; set; }
        //public object DaysToExternalRequests { get; set; }
        //public object TagContextID { get; set; }
        public bool EffortRequired { get; set; }
        public bool TemplateRequired { get; set; }
        public bool LooseEntriesAllowed { get; set; }
        //public object ProductID { get; set; }
        //public object Product { get; set; }
    }
}
