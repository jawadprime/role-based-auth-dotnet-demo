using Infrastructure.Persistence;
using Infrastructure.Persistence.DbEntities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiPresentation.Bootstrap;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        await db.Database.MigrateAsync();
        await DbSeeder.SeedAsync(db, userManager);
    }
}