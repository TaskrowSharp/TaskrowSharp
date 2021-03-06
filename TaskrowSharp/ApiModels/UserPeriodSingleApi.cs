﻿namespace TaskrowSharp.ApiModels
{
    internal class UserPeriodSingleApi
    {
        public int UserPeriodID { get; set; }
        public int UserID { get; set; }
        public int UserRelationTypeID { get; set; }
        public UserRelationTypeApi UserRelationType { get; set; }
        public DateStartEndApi DateStart { get; set; }
        public DateStartEndApi DateEnd { get; set; }
    }
}
