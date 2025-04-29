using System.Collections.Generic;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.TaskModels;

public class OwnerSequence
{
    public Owner NextOwner { get; set; }
    public Owner PrevOwner { get; set; }
    public List<Member> Members { get; set; } = [];
}
