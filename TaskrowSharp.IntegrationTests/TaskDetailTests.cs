using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class TaskDetailTests : BaseTest
    {
        private readonly TaskrowClient taskrowClient;
        private readonly List<TaskHeader> tasksList;

        public TaskDetailTests()
        {
            taskrowClient = GetTaskrowClient();

            var groupTest = taskrowClient.ListGroups().First();
            tasksList = taskrowClient.ListTasksByGroup(groupTest.GroupID);
        }

        [Fact]
        public void TaskDetail_Get_OpenTasks()
        {
            var openTasks = tasksList.Where(a => a.TaskSituation == TaskSituation.Open).OrderBy(a => a.TaskNumber).ToList();
            foreach (var task in openTasks)
            {
                var taskDetail = taskrowClient.GetTaskDetail(new TaskReference(task.ClientNickname, task.JobNumber, task.TaskNumber));

                Assert.True(task.TaskID == taskDetail.TaskID);
                Assert.True(task.TaskNumber == taskDetail.TaskNumber);
                Assert.True(string.Equals(task.TaskTitle, taskDetail.TaskTitle));

                break;
            }
        }

        [Fact]
        public void TaskDetail_Get_ClosedTasks()
        {
            var closedTasks = tasksList.Where(a => a.TaskSituation == TaskSituation.Closed).OrderBy(a => a.TaskNumber).ToList();
            foreach (var task in closedTasks)
            {
                var taskDetail = taskrowClient.GetTaskDetail(new TaskReference(task.ClientNickname, task.JobNumber, task.TaskNumber));

                Assert.True(task.TaskID == taskDetail.TaskID);
                Assert.True(task.TaskNumber == taskDetail.TaskNumber);
                Assert.True(string.Equals(task.TaskTitle, taskDetail.TaskTitle));

                break;
            }
        }
    }
}
