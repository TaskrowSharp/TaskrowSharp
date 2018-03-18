using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class MemberTaskApi
    {
        public string UserLogin { get; set; }
        public int UserID { get; set; }
        public string UserHashCode { get; set; }
        public bool Creator { get; set; }
        //public object Notify { get; set; }
        public bool MustRead { get; set; }
        public bool Read { get; set; }
        public string LastReadDate { get; set; }
        //public object Favorite { get; set; }
        public string LastModificationDate { get; set; }
    }
}
