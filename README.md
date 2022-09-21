# IntegrationTest

The sample for creating integration tests with local, in memory (not recommended) and docker container database.

# Getting started

For testing with a local database, the RecordApiFactory should look like this:

```c#
public class RecordApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.SeedDatabase(context =>
                {
                  ...
                });
            });
        }
```

For testing with a in memory database, the RecordApiFactory should look like this:

```c#
public class RecordApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Add this line
                services.UseInMemoryDbContext<DatabaseContext>();
                
                services.SeedDatabase(context =>
                {
                  ...
                });
            });
        }
```

For testing with a docker container database, the RecordApiFactory should look like this:

```c#
public class RecordApiFactory : ContainerWebApplicationFactory
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Add this line
                services.UseContainerDbContext<DatabaseContext>(DatabaseContainer);
                
                services.SeedDatabase(context =>
                {
                  ...
                });
            });
        }
```
