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
        public string BankNumber { get; set; }
        public string BankName { get; set; }
        public string BankAgencyNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountInfo { get; set; }
    }
}
