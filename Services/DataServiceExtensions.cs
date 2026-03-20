using Data.Data;
using Data.Managers;
using Data.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Services;

public static class DataServiceExtensions
{
    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Stores
        services.AddScoped<UserStore>();
        services.AddScoped<PitchStore>();
        services.AddScoped<MatchStore>();
        services.AddScoped<MatchPlayerStore>();
        services.AddScoped<ChatMessageStore>();
        services.AddScoped<NotificationStore>();

        // Managers
        services.AddScoped<PlayerManager>();
        services.AddScoped<PitchManager>();
        services.AddScoped<MatchManager>();
        services.AddScoped<MatchPlayerManager>();
        services.AddScoped<ChatMessageManager>();
        services.AddScoped<NotificationManager>();

        return services;
    }
}
