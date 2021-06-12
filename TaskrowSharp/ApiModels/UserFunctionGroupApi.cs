namespace TaskrowSharp.ApiModels
{
    internal class UserFunctionGroupApi
    {
        public int FunctionGroupID { get; set; }
        public int AppMainCompanyID { get; set; }
        public string Name { get; set; }
        public int? AppExpenseTypeID { get; set; }
        public string AppExpenseType { get; set; }
        public bool Inactive { get; set; }
        //public List<object> UserFunction { get; set; }
    }
}
