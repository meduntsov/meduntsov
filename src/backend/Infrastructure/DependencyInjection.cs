using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgres")
                               ?? "Host=localhost;Port=5432;Database=erp;Username=erp;Password=erp";

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddSingleton<IEmbeddingService, DeterministicEmbeddingService>();
        return services;
    }
}
