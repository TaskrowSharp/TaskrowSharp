using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class ActualPermissionsApi
    {
        public bool CreateTask { get; set; }
        public bool CreateSubtasks { get; set; }
        public bool AppendTaskItem { get; set; }
        public bool DelegateTask { get; set; }
        public bool ChangeDueDate { get; set; }
        public bool ChangeStatus { get; set; }
        public bool ChangeProgress { get; set; }
        public bool ChangeEffortEstimation { get; set; }
        public bool ChangeRequestContact { get; set; }
        public bool Extranet { get; set; }
        public bool CanForwardTask { get; set; }
        public bool CanEditTaskImNotOwner { get; set; }
    }
}
