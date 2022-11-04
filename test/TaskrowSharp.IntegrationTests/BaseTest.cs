using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using TaskrowSharp.IntegrationTests.TestModels;

namespace TaskrowSharp.IntegrationTests
{
    public abstract class BaseTest
    {
        private static HttpClient _httpClient = new HttpClient();
        private static string? _assemblyLocation;

        internal static ConfigurationFile GetConfigurationFile()
        {
            _assemblyLocation ??= new System.IO.FileInfo(Assembly.GetEntryAssembly()!.Location).Directory!.FullName;
            string path = System.IO.Path.Combine(_assemblyLocation, "config", "main.json");
            
            if (!System.IO.File.Exists(path))
                throw new FileNotFoundException($"File not found: config\\main.json", path);

            try
            {
                var json = System.IO.File.ReadAllText(path);
                return (JsonSerializer.Deserialize<ConfigurationFile>(json))!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading config file config\\main.json -- {ex.Message}", ex);
            }
        }

        protected static TaskrowClient GetTaskrowClient()
        {
            var config = GetConfigurationFile();
            var url = new Uri(config.ServiceUrl);
            var taskrowClient = new TaskrowClient(url, config.AccessKey, _httpClient);
            return taskrowClient;
        }
    }
}
