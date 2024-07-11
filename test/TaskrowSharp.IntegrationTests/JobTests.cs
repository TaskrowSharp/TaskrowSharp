using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;
using TaskrowSharp.Exceptions;
using System.Linq;
using System;
using TaskrowSharp.Models.JobModels;
using TaskrowSharp.Models.TaskModels;

namespace TaskrowSharp.IntegrationTests;

public class JobTests : BaseTest
{
    private readonly TaskrowClient _taskrowClient;
    private readonly ConfigurationFile _configurationFile;

    public JobTests()
    {
        _taskrowClient = GetTaskrowClient();
        _configurationFile = GetConfigurationFile();
    }

    [Fact]
    public async Task GetJobDetailAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.Where(a => a.JobNumbers.Count > 0).First();
        var jobNumbers = client.JobNumbers;

        foreach (var jobNumber in jobNumbers)
        {
            var jobEntity = await _taskrowClient.JobDetailGetAsync(client.ClientNickName, jobNumber);

            Assert.NotNull(jobEntity);
            Assert.Equal(jobNumber, jobEntity.Job.JobNumber);
        }
    }

    [Fact]
    public async Task GetJobDetailAsync_NotFound()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();
        var jobNumber = 99999;

        TaskrowException exception = null;
        try
        {
            var jobEntity = await _taskrowClient.JobDetailGetAsync(client.ClientNickName, jobNumber);
            Assert.NotNull(jobEntity);
        }
        catch (TaskrowException ex)
        {
            exception = ex;
        }
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task InsertJobAsync_Success()
    {
        var jobInsertData = _configurationFile.InsertJobData;

        var request = new JobInsertRequest(
            clientID: jobInsertData.ClientID, 
            clientNickName: jobInsertData.ClientNickName,
            jobTitle: $"{jobInsertData.JobTitle} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}",
            ownerUserID: jobInsertData.OwnerUserID,
            jobTypeID: jobInsertData.JobTypeID,
            requiredProduct: jobInsertData.RequiredProduct,
            isPublic: jobInsertData.Public,
            externalUserAccess: jobInsertData.ExternalUserAccess,
            healthReference: jobInsertData.HealthReference,
            isPrivate: jobInsertData.IsPrivate,
            effortRequired: jobInsertData.EffortRequired,
            looseEntriesAllowed: jobInsertData.LooseEntriesAllowed,
            deliverableRequired: jobInsertData.DeliverableRequired,
            requestDeliveryEnforceabilityID: jobInsertData.RequestDeliveryEnforceabilityID,
            clientAreaID: jobInsertData.ClientAreaID,
            jobSubTypeID: jobInsertData.JobSubTypeID);

        var response = await _taskrowClient.JobInsertAsync(request);
        Assert.True(response.Success);
        Assert.Equal(request.JobTitle, response.Entity.JobTitle);
        Assert.Equal(request.ClientNickName, response.Entity.Client.ClientNickName);
    }

    [Fact]
    public async Task UpdateJobAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.Where(a => a.JobNumbers.Count > 0).First();
        var jobNumbers = client.JobNumbers;
        var jobNumber = jobNumbers.First();

        var jobEntity = await _taskrowClient.JobDetailGetAsync(client.ClientNickName, jobNumber);

        var request = new JobUpdateRequest(jobEntity.Job);

        var response = await _taskrowClient.JobUpdateAsync(request);
        Assert.True(response.Success);
        Assert.Equal(request.JobNumber, response.Entity.JobNumber);
        Assert.Equal(request.JobTitle, response.Entity.JobTitle);
    }

    [Fact]
    public async Task ListJobClientDependeciesAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();

        var response = await _taskrowClient.JobClientDependecyListAsync(client.ClientID);

        Assert.NotNull(response);
        Assert.NotEmpty(response.Entity.ClientAreas);
    }

    [Fact]
    public async Task UpdateJobStatusAsyncAsync_Inactivate_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();
        var jobNumber = client.JobNumbers.First();

        var jobStatusID = 4;

        var response = await _taskrowClient.JobStatusUpdateAsync(client.ClientNickName, jobNumber, jobStatusID);

        Assert.NotNull(response);
        Assert.True(response.Success);
        Assert.Equal(jobStatusID, response.Entity.JobStatusID);
    }

    [Fact]
    public async Task GetJobHomeAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();
        var jobNumber = client.JobNumbers.First();

        var jobHome = await _taskrowClient.JobHomeGetAsync(client.ClientNickName, jobNumber);

        Assert.NotNull(jobHome);
        Assert.NotNull(jobHome.Job);
        Assert.Equal(jobNumber, jobHome.Job.JobNumber);
    }

    [Fact]
    public async Task SaveJobWallPostAsync_Success()
    {
        if (_configurationFile.Clients?.Count == 0)
            throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");

        var client = _configurationFile.Clients.First();
        var jobNumber = client.JobNumbers.First();
        var jobHome = await _taskrowClient.JobHomeGetAsync(client.ClientNickName, jobNumber);

        var request = new JobWallPostSaveRequest(jobHome.JobWall.WallID, jobNumber, $"TaskrowSharp Test -- {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff)}");
        var response = await _taskrowClient.JobWallPostSaveAsync(request);

        Assert.NotNull(response);
        Assert.True(response.Success);
    }
}
