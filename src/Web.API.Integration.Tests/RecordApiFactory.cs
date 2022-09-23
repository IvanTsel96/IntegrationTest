using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Web.API.Integration.Tests
{
    public class RecordApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // Use for test with memory database
                // services.UseInMemoryDbContext<DatabaseContext>();

                // Use for test with container database and
                // This class should inherit from ContainerWebApplicationFactory
                // services.UseContainerDbContext<DatabaseContext>(DatabaseContainer);
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
