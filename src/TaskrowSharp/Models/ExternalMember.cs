namespace TaskrowSharp.Models
{
    public class ExternalMember
    {
        public int TaskExternalMemberID { get; set; }
        public bool Read { get; set; }
        public string? LastReadDate { get; set; }
        public Contact? Contact { get; set; }
    }
}
