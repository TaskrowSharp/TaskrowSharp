using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    public class SaveTaskResponseApi
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string TargetURL { get; set; }

        public string PreviousGUID { get; set; }

        public string UserTaskListGUID { get; set; }

        public SaveTaskResponseApi()
        {

        }

        public SaveTaskResponseApi(SaveTaskResponse response)
        {
            this.Success = response.Success;
            this.Message = response.Message;
            this.TargetURL = response.TargetURL;
            this.PreviousGUID = response.PreviousGUID;
            this.UserTaskListGUID = response.UserTaskListGUID;
        }
    }
}
