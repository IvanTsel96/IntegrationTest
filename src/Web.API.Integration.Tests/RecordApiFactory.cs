using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Web.API.Integration.Tests.Extensions;
using Web.API.Integration.Tests.Mock;
using Record = Web.API.Domain.Record;

namespace Web.API.Integration.Tests
{
    public class RecordApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Use for test with memory database
                // services.UseInMemoryDbContext<DatabaseContext>();

                // Use for test with container database and
                // This class should inherit from ContainerWebApplicationFactory
                // services.UseContainerDbContext<DatabaseContext>(DatabaseContainer);

                services.SeedDatabase(context =>
                {
                    if (!context.Set<Record>().Any(x => x.Id == RecordMockHelper.IdToGet))
                    {
                        context.Add(RecordMockHelper.GetRecord(RecordMockHelper.IdToGet, "GET"));
                    }

                    if (!context.Set<Record>().Any(x => x.Id == RecordMockHelper.IdToUpdate))
                    {
                        context.Add(RecordMockHelper.GetRecord(RecordMockHelper.IdToUpdate, "PUT"));
                    }

                    if (!context.Set<Record>().Any(x => x.Id == RecordMockHelper.IdToDelete))
                    {
                        context.Add(RecordMockHelper.GetRecord(RecordMockHelper.IdToDelete, "DELETE"));
                    }
                });
            });
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            // TODO: What with logger?
            return Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
