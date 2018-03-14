using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_List()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var listUsers = taskrowClient.ListUsers();
            Assert.IsTrue(listUsers.Count > 0);
        }

        [TestMethod]
        public void User_GetByEmail()
        {
            var config = UtilsTest.GetConfigurationFile();
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var user = taskrowClient.GetUserByEmail(config.Email);
            Assert.IsTrue(user != null);
            Assert.IsTrue(string.Equals(config.Email, user.MainEmail));
        }
    }
}
