using F1GameDataParser.Mapping.ViewModelBuilders;
using F1GameDataParser.ViewModels.TimingTower;
using Microsoft.Extensions.DependencyInjection;

namespace F1GameDataParser.Startup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<IViewModelBuilder<TimingTower> ,TimingTowerBuilder>();
            return services;
        }
    }
}
