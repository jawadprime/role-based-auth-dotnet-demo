namespace MinimalApi.Bootstrap;

public static partial class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure(configuration)
            .AddApplication(configuration);
    }
}