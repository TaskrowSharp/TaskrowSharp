using System;

namespace TaskrowSharp
{
    public class UserFunctionPeriod
    {
        public int UserFunctionPeriodID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int UserFunctionID { get; set; }
        public string UserFunctionTitle { get; set; }
        public int OfficeID { get; set; }
        //public Office Office { get; set; }
        //public object RegistrationNumber { get; set; }
        public int UserRelationTypeID { get; set; }
        //public UserRelationType UserRelationType { get; set; }
        //public object FunctionRate { get; set; }
        //public object FunctionMargin { get; set; }
        //public object NetValue { get; set; }
        //public object GrossValue { get; set; }
        //public object UserTerminationTypeID { get; set; }
        //public string UserTerminationType { get; set; }
        public string TerminationMemo { get; set; }

        public UserFunctionPeriod(int userFunctionPeriodID, DateTime dateStart, DateTime? dateEnd,
            int userFunctionID, string userFunctionTitle, int officeID, int userRelationTypeID, string terminationMemo)
        {
            this.UserFunctionPeriodID = userFunctionPeriodID;
            this.DateStart = dateStart;
            this.DateEnd = dateEnd;
            this.UserFunctionID = userFunctionID;
            this.UserFunctionTitle = userFunctionTitle;
            this.OfficeID = officeID;
            this.UserRelationTypeID = userRelationTypeID;
            this.TerminationMemo = terminationMemo;
        }

        internal UserFunctionPeriod(ApiModels.UserFunctionPeriodApi userFunctionPeriodApi)
        {
            this.UserFunctionPeriodID = userFunctionPeriodApi.UserFunctionPeriodID;
            this.DateStart = Utils.Parser.ToDateTimeFromTaskrowDate(userFunctionPeriodApi.DateStart);
            this.DateEnd = (userFunctionPeriodApi.DateEnd != null ? (DateTime?) Utils.Parser.ToDateTimeFromTaskrowDate(userFunctionPeriodApi.DateEnd) : null);
            this.UserFunctionID = userFunctionPeriodApi.UserFunctionID;
            this.UserFunctionTitle = userFunctionPeriodApi.UserFunctionTitle;
            this.OfficeID = userFunctionPeriodApi.OfficeID;
            this.UserRelationTypeID = userFunctionPeriodApi.UserRelationTypeID;
            this.TerminationMemo = userFunctionPeriodApi.TerminationMemo;
        }
    }
}
