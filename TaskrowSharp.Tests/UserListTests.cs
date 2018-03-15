using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class UserListTests
    {
        [TestMethod]
        public void UserList_OK()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var listUsers = taskrowClient.ListUsers();
            Assert.IsTrue(listUsers.Count > 0);
        }
    }
}
