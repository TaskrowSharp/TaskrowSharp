using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class OfficeApi
    {
        public int OfficeID { get; set; }
        public string Name { get; set; }
        public bool MainOffice { get; set; }
        //public object DailyMinutes { get; set; }
        public string ExternalCode { get; set; }
        public int AppMainCompanyID { get; set; }
        public string CreationDate { get; set; }
        public int CreationUserID { get; set; }
        public string LastModificationDate { get; set; }
        public int LastModificationUserID { get; set; }
        public bool Inactive { get; set; }
    }
}
