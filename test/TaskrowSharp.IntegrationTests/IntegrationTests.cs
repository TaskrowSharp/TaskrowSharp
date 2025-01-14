using System.Linq;
using System.Threading.Tasks;
using System;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;
using TaskrowSharp.Models.Integration;

namespace TaskrowSharp.IntegrationTests;

public class IntegrationTests : BaseTest
{
    private readonly TaskrowClient _taskrowClient;
    private readonly ConfigurationFile _configurationFile;

    public IntegrationTests()
    {
        _taskrowClient = GetTaskrowClient();
        _configurationFile = GetConfigurationFile();
    }

    [Fact]
    public async Task IntegrationLogInsertAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();

        if (client.JobNumbers?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"client[0].jobNumbers\" list is empty");
        
        var jobNumber = client.JobNumbers?.FirstOrDefault() ?? 0;

        var job = await _taskrowClient.JobDetailGetAsync(client.ClientNickName, jobNumber);
        if (job == null)
            throw new System.InvalidOperationException($"Error in configuration file, \"client[0].jobNumbers[0]\" job not found -- clientNickName={client.ClientNickName}, jobNumber={jobNumber}");

        var dateReference = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        const string entityType = "job";
        var entityID = job.JobID.ToString();
        var userMessage = $"UserMessage {dateReference}";

        var request = new IntegrationLogInsertRequest(entityType, entityID,  
            new IntegrationLogInsertRequestEntry(userMessage, $"UserDetail {dateReference}", $"TechDetail {dateReference}", DateTimeOffset.Now, IntegrationLogEntryLevels.Info));

        await _taskrowClient.IntegrationLogInsertAsync(request);
        
        var logEntries = await _taskrowClient.IntegrationLogListAsync(entityType, entityID);
        Assert.NotNull(logEntries);
        Assert.NotEmpty(logEntries);
        Assert.Equal(userMessage, logEntries.Last().UserMessage);
    }
}
