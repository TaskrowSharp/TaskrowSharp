using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserGroupApi
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int OwnerUserID { get; set; }
    }
}
