using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserFunctionPeriodApi
    {
        public int UserFunctionPeriodID { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public int UserFunctionID { get; set; }
        public string UserFunctionTitle { get; set; }
        public int OfficeID { get; set; }
        public OfficeApi Office { get; set; }
        //public object RegistrationNumber { get; set; }
        public int UserRelationTypeID { get; set; }
        public UserRelationTypeApi UserRelationType { get; set; }
        //public object FunctionRate { get; set; }
        //public object FunctionMargin { get; set; }
        //public object NetValue { get; set; }
        //public object GrossValue { get; set; }
        //public object UserTerminationTypeID { get; set; }
        //public string UserTerminationType { get; set; }
        public string TerminationMemo { get; set; }
    }
}
