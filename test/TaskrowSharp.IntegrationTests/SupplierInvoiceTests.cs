using System.Diagnostics;
using System;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;

namespace TaskrowSharp.IntegrationTests;

public class SupplierInvoiceTests : BaseTest
{
    private readonly TaskrowClient _taskrowClient;
    private readonly ConfigurationFile _configurationFile;

    public SupplierInvoiceTests()
    {
        _taskrowClient = GetTaskrowClient();
        _configurationFile = GetConfigurationFile();
    }

    [Fact]
    public async Task GetSupplierInvoice_Success()
    {
        try
        {
            var supplierInvoiceIds = _configurationFile.SupplierInvoiceIDs;

            foreach (var supplierInvoiceID in supplierInvoiceIds)
            {
                var supplierInvoice = await _taskrowClient.SupplierInvoiceGetAsync(supplierInvoiceID);
                Assert.NotNull(supplierInvoice);
                Assert.Equal(supplierInvoiceID, supplierInvoice.SupplierInvoice.SupplierInvoiceID);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($">> ERROR: {ex.Message}");
            throw;
        }
    }
}