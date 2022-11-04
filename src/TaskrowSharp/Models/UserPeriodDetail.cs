namespace TaskrowSharp.Models
{
    public class UserPeriodDetail
    {
        public int UserPeriodID { get; set; }
        public int UserID { get; set; }
        public int UserRelationTypeID { get; set; }
        public UserRelationType? UserRelationType { get; set; }
        public DateStartEntity? DateStart { get; set; }
        public DateEndEntity? DateEnd { get; set; }
    }
}
