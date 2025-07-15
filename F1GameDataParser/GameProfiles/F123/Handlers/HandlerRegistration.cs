using Microsoft.Extensions.DependencyInjection;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public static class HandlerRegistration
    {
        public static IServiceCollection AddF123Handlers(this IServiceCollection services)
        {
            services.AddSingleton<ParticipantsHandler>();
            services.AddSingleton<SessionHandler>();
            services.AddSingleton<CarTelemetryHandler>();
            services.AddSingleton<EventsHandler>();
            services.AddSingleton<CarStatusHandler>();
            services.AddSingleton<FinalClassificationHandler>();
            services.AddSingleton<LapHandler>();
            services.AddSingleton<SessionHistoryHandler>();
            services.AddSingleton<CarDamageHandler>();
            services.AddSingleton<LobbyInfoHandler>();

            return services;
        }
    }
}
