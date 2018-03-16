using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TaskrowSharp.IntegrationTests
{
    [TestFixture]
    public class TaskDetailTests
    {
        TaskrowClient taskrowClient;
        List<Task> tasksList;

        [OneTimeSetUp]
        public void Setup()
        {
            taskrowClient = UtilsTest.GetTaskrowClient();

            var groupTest = taskrowClient.ListGroups().First();
            tasksList = taskrowClient.ListTasksByGroup(groupTest.GroupID);
        }

        [Test]
        public void TaskDetail_Get_OpenTasks()
        {
            var openTasks = tasksList.Where(a => a.TaskStatus == TaskStatus.Open).OrderBy(a => a.TaskNumber).Take(5).ToList();
            foreach (var task in openTasks)
            {
                var taskDetail = taskrowClient.GetTaskDetail(task.JobNumber, task.TaskNumber, task.ClientNickName);

                Assert.IsTrue(task.TaskID == taskDetail.TaskID);
                Assert.IsTrue(task.TaskNumber == taskDetail.TaskNumber);
                Assert.IsTrue(string.Equals(task.TaskTitle, taskDetail.TaskTitle));
            }
        }

        [Test]
        public void TaskDetail_Get_ClosedTasks()
        {
            var closedTasks = tasksList.Where(a => a.TaskStatus == TaskStatus.Closed).OrderBy(a => a.TaskNumber).Take(5).ToList();
            foreach (var task in closedTasks)
            {
                var taskDetail = taskrowClient.GetTaskDetail(task.JobNumber, task.TaskNumber, task.ClientNickName);

                Assert.IsTrue(task.TaskID == taskDetail.TaskID);
                Assert.IsTrue(task.TaskNumber == taskDetail.TaskNumber);
                Assert.IsTrue(string.Equals(task.TaskTitle, taskDetail.TaskTitle));
            }
        }
    }
}
