using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Web.API.Integration.Tests
{
    public class ContainerWebApplicationFactory : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        protected readonly PostgreSqlTestcontainer DatabaseContainer;

        protected ContainerWebApplicationFactory()
        {
            DatabaseContainer = new TestcontainersBuilder<PostgreSqlTestcontainer>()
                .WithDatabase(new PostgreSqlTestcontainerConfiguration
                {
                    Database = "recordDb",
                    Username = "postgres",
                    Password = "password"
                })
                .Build();
        }
        
        public async Task InitializeAsync()
        {
            await DatabaseContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await DatabaseContainer.StopAsync();
        }
    }
}
