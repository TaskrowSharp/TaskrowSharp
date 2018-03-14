using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserBankingModelApi
    {
        public int UserBankingInfoID { get; set; }
        public int UserID { get; set; }
        public object BankNumber { get; set; }
        public object BankName { get; set; }
        public object BankAgencyNumber { get; set; }
        public object BankAccountNumber { get; set; }
        public object BankAccountInfo { get; set; }
    }
}
