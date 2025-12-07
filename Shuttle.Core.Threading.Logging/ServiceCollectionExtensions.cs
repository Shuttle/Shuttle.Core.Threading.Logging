using Microsoft.Extensions.DependencyInjection;

namespace Shuttle.Core.Threading.Logging;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddThreadingLogging()
        {
            services.AddHostedService<ThreadingLogger>();

            return services;
        }
    }
}