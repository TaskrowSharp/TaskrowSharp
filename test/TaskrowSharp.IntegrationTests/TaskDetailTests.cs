using System;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.Models;
using TaskrowSharp.Models.TaskModels;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class TaskDetailTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;
        private static readonly Random _random = new();
        private const int LIMIT = 10;

        public TaskDetailTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task TaskDetailGetAsync_OpenTasks()
        {
            var group = (await _taskrowClient.UserGroupListAsync()).First();
            var tasksByGroup = await _taskrowClient.TaskListByGroupAsync(group.GroupID);

            var openTasks = tasksByGroup.OpenTasks.OrderBy(a => _random.Next()).Take(LIMIT).ToList();
            foreach (var task in openTasks)
            {
                var taskReference = new TaskReference(task.ClientNickName, task.JobNumber, task.TaskNumber);
                var taskDetailResponse = await _taskrowClient.TaskDetailGetAsync(taskReference);

                Assert.Equal(taskReference.ClientNickname, taskDetailResponse.JobData.Client.ClientNickName);
                Assert.Equal(taskReference.JobNumber, taskDetailResponse.JobData.JobNumber);
                Assert.Equal(taskReference.TaskNumber, taskDetailResponse.TaskData.TaskNumber);
            }

            var closedTasks = tasksByGroup.ClosedTasks.OrderBy(a => _random.Next()).Take(LIMIT).ToList();
            foreach (var task in closedTasks)
            {
                var taskReference = new TaskReference(task.ClientNickName, task.JobNumber, task.TaskNumber);
                var taskDetailResponse = await _taskrowClient.TaskDetailGetAsync(taskReference);

                Assert.Equal(taskReference.ClientNickname, taskDetailResponse.JobData.Client.ClientNickName);
                Assert.Equal(taskReference.JobNumber, taskDetailResponse.JobData.JobNumber);
                Assert.Equal(taskReference.TaskNumber, taskDetailResponse.TaskData.TaskNumber);

            }
        }
    }
}
