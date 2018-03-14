using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserListApiResponse
    {
        public List<UserApi> Users { get; set; }
        public PermissionsApi Permissions { get; set; }
        public object UsagePlanLimitUsers { get; set; }
    }
}
