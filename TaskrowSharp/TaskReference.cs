using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp
{
    public class TaskReference
    {
        public string ClientNickname { get; set; }

        public int JobNumber { get; set; }

        public int TaskNumber { get; set; }

        public TaskReference(string clientNickname, int jobNumber, int taskNumber)
        {
            this.ClientNickname = clientNickname;
            this.JobNumber = jobNumber;
            this.TaskNumber = taskNumber;
        }

        public TaskReference(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(nameof(url));

            //ex: https://mycompary.taskrow.com/#taskcentral/MyClient/488/1501
                        
            int index = url.IndexOf("#taskcentral", StringComparison.CurrentCultureIgnoreCase);
            if (index > 0)
                url = url.Substring(index + "#taskcentral".Length);

            //ex: /MyClient/488/1501
            if (url.StartsWith("/"))
                url = url.Substring(1);

            var array = url.Split('/');
            if (array.Length < 3)
                throw new ArgumentException(string.Format("Invalid task url \"{0}\". Please use the format: /{{MyClient}}/{{JobNumber}}/{{TaskNumber}}", url));

            this.ClientNickname = array[0];

            this.JobNumber = Utils.Parser.ToInt32(array[1]);
            if (this.JobNumber == 0)
                throw new ArgumentException(string.Format("JobNumber invalid in task url \"{0}\". Please use the format: /{{MyClient}}/{{JobNumber}}/{{TaskNumber}}", url));

            this.TaskNumber = Utils.Parser.ToInt32(array[2]);
            if (this.TaskNumber == 0)
                throw new ArgumentException(string.Format("TaskNumber invalid in task url \"{0}\". Please use the format: /{{MyClient}}/{{JobNumber}}/{{TaskNumber}}", url));
        }
    }
}
