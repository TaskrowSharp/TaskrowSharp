using System.Threading.Tasks;
using System;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models.JobModels;
using Xunit;
using System.Linq;

namespace TaskrowSharp.IntegrationTests;

public class OpportunityTests : BaseTest
{
    private readonly TaskrowClient _taskrowClient;
    private readonly ConfigurationFile _configurationFile;

    public OpportunityTests()
    {
        _taskrowClient = GetTaskrowClient();
        _configurationFile = GetConfigurationFile();
    }

    [Fact]
    public async Task InsertOpportunityAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();

        var request = new InsertOpportunityRequest(
            clientID: client.ClientID,
            clientNickName: client.ClientNickName,
            name: $"Oportunidade_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}",
            description: "Teste Taskrow");

        var response = await _taskrowClient.InsertOpportunityAsync(request);
        Assert.True(response.Success);
        Assert.Equal(request.Name, response.Entity.Opportunity.Name);
    }
}
