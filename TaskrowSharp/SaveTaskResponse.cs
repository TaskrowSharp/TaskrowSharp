namespace TaskrowSharp
{
    public class SaveTaskResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TargetURL { get; set; }
        public string PreviousGUID { get; set; }
        public string UserTaskListGUID { get; set; }

        public SaveTaskResponse()
        {

        }

        public SaveTaskResponse(ApiModels.SaveTaskResponseApi api)
        {
            this.Success = api.Success;
            this.Message = api.Message;
            this.TargetURL = api.TargetURL;
            this.PreviousGUID = api.PreviousGUID;
            this.UserTaskListGUID = api.UserTaskListGUID;
        }
    }
}
