using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TaskrowSharp.Tests
{
    [TestFixture]
    public class GroupListTests
    {
        TaskrowClient taskrowClient;

        [OneTimeSetUp]
        public void Setup()
        {
            taskrowClient = UtilsTest.GetTaskrowClient();
        }

        [Test]
        public void GroupList_OK()
        {
            var listGroups = taskrowClient.ListGroups();
            Assert.IsTrue(listGroups.Count > 0);
        }
    }
}
