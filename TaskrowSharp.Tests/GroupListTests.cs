using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class GroupListTests
    {
        [TestMethod]
        public void GroupList_OK()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var listGroups = taskrowClient.ListGroups();
            Assert.IsTrue(listGroups.Count > 0);
        }
    }
}
