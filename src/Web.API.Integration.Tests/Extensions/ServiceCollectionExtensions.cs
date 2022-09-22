using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Web.API.Infrastructure;

namespace Web.API.Integration.Tests.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        private static readonly object Lock = new();

        internal static IServiceCollection UseInMemoryDbContext<T>(this IServiceCollection services)
            where T : DbContext
        {
            services.RemoveDbContext<T>();

            services.AddDbContext<T>(options => options.UseInMemoryDatabase("InMemoryRecords"));

            return services;
        }

        internal static IServiceCollection UseContainerDbContext<T>(this IServiceCollection services, PostgreSqlTestcontainer container)
            where T : DbContext
        {
            services.RemoveDbContext<T>();

            services.AddDbContext<T>(options => options.UseNpgsql(container.ConnectionString));

            return services;
        }

        internal static void SeedDatabase(this IServiceCollection service, Action<DbContext> seedAction)
        {
            lock (Lock)
            {
                var serviceProvider = service.BuildServiceProvider();

                using var context = serviceProvider.GetRequiredService<DatabaseContext>();

                seedAction?.Invoke(context);

                context.SaveChanges();
            }
        }

        private static IServiceCollection RemoveDbContext<T>(this IServiceCollection services)
            where T : DbContext
        {
            var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));

            if (dbContext != null)
            {
                services.Remove(dbContext);
            }

            return services;
        }
    }
}
