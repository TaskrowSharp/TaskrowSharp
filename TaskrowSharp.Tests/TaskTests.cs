using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void ListTasksOpenByGroup_OK()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var groupTest = taskrowClient.ListGroups().First();

            var tasks = taskrowClient.ListTasksByGroup(groupTest.GroupID);
            Assert.IsTrue(tasks.Count > 0);
        }
    }
}
