using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class TaskTag
    {
        public int TaskTagID { get; set; }

        public string TagTitle { get; set; }

        public TaskTag(int taskTagID, string tagTitle)
        {
            this.TaskTagID = taskTagID;
            this.TagTitle = tagTitle;
        }

        internal TaskTag(ApiModels.TagApi tagApi)
        {
            this.TaskTagID = tagApi.TaskTagID;
            this.TagTitle = tagApi.TagTitle;
        }
    }
}
