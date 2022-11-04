using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class UserFunctionGroup
    {
        public int FunctionGroupID { get; set; }
        public int AppMainCompanyID { get; set; }
        public string? Name { get; set; }
        public int AppExpenseTypeID { get; set; }
        public string? AppExpenseType { get; set; }
        public bool Inactive { get; set; }
        public List<object> UserFunction { get; set; }
        public int UserFunctionMaxOrder { get; set; }
        public int UserFunctionMinOrder { get; set; }
        public int Order { get; set; }
        public string? DisplayName { get; set; }
        public int CostCenterID { get; set; }
        public string? CostCenterName { get; set; }
    }
}
