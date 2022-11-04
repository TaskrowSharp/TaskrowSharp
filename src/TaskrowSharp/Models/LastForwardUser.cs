namespace TaskrowSharp.Models
{
    public class LastForwardUser
    {
        public int UserID { get; set; }
        public string? UserLogin { get; set; }
        public string? UserHashCode { get; set; }
        public string? FullName { get; set; }
        //public object? ExternalCode { get; set; }
        public int PhotoVersion { get; set; }
        public bool Creator { get; set; }
        public string? ApprovalGroup { get; set; }
    }
}
