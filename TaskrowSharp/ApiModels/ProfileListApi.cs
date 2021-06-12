namespace TaskrowSharp.ApiModels
{
    internal class ProfileListApi
    {
        public int AppMainCompanyID { get; set; }
        public int ProfileID { get; set; }
        public int ProfileRate { get; set; }
        public string ProfileTitle { get; set; }
        public bool BuiltInUser { get; set; }
        public string PermissionListString { get; set; }
        public bool Inactive { get; set; }
        public bool? ExternalUser { get; set; }
        //public List<object> Permission { get; set; }
        //public List<object> Users { get; set; }
    }
}
