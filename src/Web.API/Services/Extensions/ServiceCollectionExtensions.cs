using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.API.Infrastructure;
using Web.API.Infrastructure.Repositories.Records;

namespace Web.API.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureReferences(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("RecordContext")));

            services.AddScoped<IRecordRepository, RecordRepository>();

            return services;
        }
    }
}
