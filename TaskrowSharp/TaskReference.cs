using System;

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

            //ex: https://mycompary.taskrow.com/#taskcentral/ClientNickName/488/1501
            
            int index = url.IndexOf("#taskcentral", StringComparison.CurrentCultureIgnoreCase);
            if (index > 0)
                url = url.Substring(index + "#taskcentral".Length);

            //ex: /ClientNickName/488/1501
            if (url.StartsWith("/"))
                url = url.Substring(1);

            var array = url.Split('/');
            if (array.Length < 3)
                throw new ArgumentException($"Invalid task url \"{url}\". Please use the format: /{{ClientNickName}}/{{JobNumber}}/{{TaskNumber}}");

            this.ClientNickname = array[0];

            this.JobNumber = Utils.Parser.ToInt32(array[1]);
            if (this.JobNumber == 0)
                throw new ArgumentException($"JobNumber invalid in task url \"{url}\". Please use the format: /{{ClientNickName}}/{{JobNumber}}/{{TaskNumber}}");

            this.TaskNumber = Utils.Parser.ToInt32(array[2]);
            if (this.TaskNumber == 0)
                throw new ArgumentException($"TaskNumber invalid in task url \"{url}\". Please use the format: /{{ClientNickName}}/{{JobNumber}}/{{TaskNumber}}");
        }
    }
}
