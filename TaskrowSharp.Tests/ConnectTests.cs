using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class ConnectTests
    {
        [TestMethod]
        public void Connect_EmailAndPasswordCredential_OK()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri(configurationFile.ServiceUrl), configurationFile.Email, configurationFile.Password);

            client.Disconnect();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidServiceUrlException))]
        public void Connect_UserPasswordCredential_ServiceUrl_HttpsRequired()
        {
            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri("http://yourcompany.taskrow.com"), "test@test.com", "123456789");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidServiceUrlException))]
        public void Connect_UserPasswordCredential_ServiceUrl_Invalid()
        {
            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri("http://test.com"), "test@test.com", "123456789");
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void Connect_UserPasswordCredential_WrongPassword()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri(configurationFile.ServiceUrl), configurationFile.Email, "123456789");
        }
    }
}
