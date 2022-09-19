using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.API.Services.Extensions;
using Web.API.Services.Records;

namespace Web.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceReferences(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRecordService, RecordService>();

            return services.AddInfrastructureReferences(configuration);
        }
    }
}
