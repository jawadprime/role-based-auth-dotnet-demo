using Application.Abstractions;
using Domain;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.DbEntities.ProjectMember;
using Infrastructure.Persistence.DbEntities.Roles;
using Infrastructure.Persistence.DbEntities.Users;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace MinimalApi;

public static partial class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions(configuration);

        services.AddDatabase(configuration);

        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.SectionName))
            .ValidateDataAnnotations();

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var dbOptions = sp
                .GetRequiredService<IOptions<DatabaseOptions>>()
                .Value;

            options.UseNpgsql(dbOptions.ApplicationDb);
        });

        services.AddIdentity<UserEntity, RoleEntity>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(Repository<>), typeof(Repository<>));
        services.AddScoped<ProjectRepo, ProjectRepoImpl>();
        services.AddScoped<MembershipRepo<ProjectId>, ProjectMembershipRepoImpl>();
        services.AddScoped(typeof(AuthorizationService<>), typeof(AuthorizationServiceImpl<>));

        return services;
    }
}