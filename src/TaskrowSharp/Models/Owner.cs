namespace TaskrowSharp.Models
{
    public class Owner
    {
        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string FullName { get; set; }
        public string UserHashCode { get; set; }
        public int ProfileID { get; set; }
        public string? MainEmail { get; set; }
        public string? Extension { get; set; }
        public bool? SystemUser { get; set; }
        public bool? Inactive { get; set; }
        public bool? Creator { get; set; }
        public string? ApprovalGroup { get; set; }
    }
}
