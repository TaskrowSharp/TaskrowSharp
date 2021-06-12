namespace TaskrowSharp.ApiModels
{
    internal class RequestTypeListApi
    {
        public int RequestTypeID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Acronym { get; set; }
        public bool Extranet { get; set; }
        public bool IsDefault { get; set; }
        public string ExternalCode { get; set; }
        public int AppMainCompanyID { get; set; }
        //public object ColorID { get; set; }
    }
}
