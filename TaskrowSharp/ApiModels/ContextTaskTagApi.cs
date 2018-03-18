using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class ContextTaskTagApi
    {
        //public string $id { get; set; }
        public int TaskTagID { get; set; }
        public int TaskID { get; set; }
        public int JobID { get; set; }
        public string TagTitle { get; set; }
        public string TagColor { get; set; }
        //public object Job { get; set; }
        //public object Task { get; set; }
        public int CountTasks { get; set; }
    }
}
