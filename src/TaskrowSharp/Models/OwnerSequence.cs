using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class OwnerSequence
    {
        public Owner NextOwner { get; set; }
        public Owner PrevOwner { get; set; }
        public List<Member> Members { get; set; }
    }
}
