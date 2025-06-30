using F1GameDataParser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace F1GameDataParser.Startup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<ITimingTowerService, TimingTowerService>();
            services.AddTransient<EventService>();

            return services;
        }
    }
}
