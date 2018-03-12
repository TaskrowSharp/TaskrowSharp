using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private ConfigurationFile GetConfigurationFile()
        {
            string path = Utils.IOHelper.GetFullPathFromRelative("config/main.json", true, true);

            string json;
            using (var reader = new System.IO.StreamReader(path))
            {
                json = reader.ReadToEnd();
            }
                        
            var configurationFile = Utils.JsonHelper.Deserialize<ConfigurationFile>(json);
            
            return configurationFile;
        }

        [TestMethod]
        public void Connect()
        {
            var configurationFile = GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(configurationFile.ServiceUrl, new TaskrowSharp.Credential(configurationFile.Email, configurationFile.Password));

            client.Disconnect();
        }
    }
}
