using System;

namespace TaskrowSharp.Models.TaskModels;

public class TaskReference
{
    public string ClientNickname { get; set; }
    public int JobNumber { get; set; }
    public int TaskNumber { get; set; }

    public TaskReference(string clientNickName, int jobNumber, int taskNumber)
    {
        ClientNickname = clientNickName;
        JobNumber = jobNumber;
        TaskNumber = taskNumber;
    }

    public TaskReference(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException(nameof(url));

        //ex: https://mycompary.taskrow.com/#taskcentral/MyClient/488/1501

        int index = url.IndexOf("#taskcentral", StringComparison.CurrentCultureIgnoreCase);
        if (index > 0)
            url = url.Substring(index + "#taskcentral".Length);

        //ex: /MyClient/488/1501
        if (url.StartsWith("/"))
            url = url.Substring(1);

        var array = url.Split('/');
        if (array.Length < 3)
            throw new ArgumentException($"Invalid task url \"{url}\". Please use the format: /{{MyClient}}/{{JobNumber}}/{{TaskNumber}}");

        ClientNickname = array[0];

        JobNumber = int.Parse(array[1]);
        if (JobNumber == 0)
            throw new ArgumentException($"JobNumber invalid in task url \"{url}\". Please use the format: /{{MyClient}}/{{JobNumber}}/{{TaskNumber}}");

        TaskNumber = int.Parse(array[2]);
        if (TaskNumber == 0)
            throw new ArgumentException($"TaskNumber invalid in task url \"{url}\". Please use the format: /{{MyClient}}/{{JobNumber}}/{{TaskNumber}}");
    }
}
