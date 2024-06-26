using System.Threading.Tasks;
using System;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;
using System.Linq;
using TaskrowSharp.Models.OpportunityModels;

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
    public async Task OpportunityInsertAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();

        var request = new OpportunityInsertRequest(
            clientID: client.ClientID,
            clientNickName: client.ClientNickName,
            name: $"Oportunidade_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}",
            description: "Teste Taskrow");

        var opportunity = await _taskrowClient.OpportunityInsertAsync(request);
        Assert.NotNull(opportunity);
        Assert.Equal(request.Name, opportunity.Name);

        var opportunityGet = await _taskrowClient.OpportunityGetAsync(client.ClientNickName, opportunity.OpportunityID);
        Assert.NotNull(opportunityGet);
        Assert.Equal(request.Name, opportunityGet.Name);
    }

    [Fact]
    public async Task OpportunityTransferToClientAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");
        if (_configurationFile.Clients?.Count < 2)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" should have at least 2 items");

        var client1 = _configurationFile.Clients.First();
        var client2 = _configurationFile.Clients.Skip(1).First();

        var insertRequest = new OpportunityInsertRequest(
            clientID: client1.ClientID,
            clientNickName: client1.ClientNickName,
            name: $"Oportunidade_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}",
            description: "Teste Taskrow");
        var insertResponse = await _taskrowClient.OpportunityInsertAsync(insertRequest);

        var transferRequest = new OpportunityTransferToClientRequest(
            clientNickName: client1.ClientNickName, 
            opportunityID: insertResponse.OpportunityID, 
            newClientNickName: client2.ClientNickName);
        var opportunity = await _taskrowClient.OpportunityTransferToClientAsync(transferRequest);

        Assert.NotNull(opportunity);
        Assert.Equal(client2.ClientNickName, opportunity.Client.ClientNickName);
    }
}
