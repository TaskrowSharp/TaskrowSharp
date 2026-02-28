using System.Diagnostics;
using System;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;
using System.Linq;

namespace TaskrowSharp.IntegrationTests;

public class SupplierOrderTests : BaseTest
{
    private readonly TaskrowClient _taskrowClient;
    private readonly ConfigurationFile _configurationFile;

    public SupplierOrderTests()
    {
        _taskrowClient = GetTaskrowClient();
        _configurationFile = GetConfigurationFile();
    }

    [Fact]
    public async Task GetSupplierOrder_Success()
    {
        try
        {
            var supplierOrderIds = _configurationFile.SupplierOrderIDs;

            foreach (var supplierOrderID in supplierOrderIds)
            {
                var supplierOrder = await _taskrowClient.SupplierOrderGetAsync(supplierOrderID);
                Assert.NotNull(supplierOrder);
                Assert.Equal(supplierOrderID, supplierOrder.SupplierOrderID);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($">> ERROR: {ex.Message}");
            throw;
        }
    }
}