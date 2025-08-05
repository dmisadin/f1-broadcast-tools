using F1GameDataParser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace F1GameDataParser.Startup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<EventService>();
            services.AddTransient<LapTimeService>();
            services.AddTransient<LapService>();

            return services;
        }
    }
}
