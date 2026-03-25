using Domain;
using Domain.Constants;
using Infrastructure.Persistence;
using Infrastructure.Persistence.DbEntities.Permissions;
using Infrastructure.Persistence.DbEntities.ProjectMember;
using Infrastructure.Persistence.DbEntities.Projects;
using Infrastructure.Persistence.DbEntities.RolePermissions;
using Infrastructure.Persistence.DbEntities.Roles;
using Infrastructure.Persistence.DbEntities.Users;
using Microsoft.AspNetCore.Identity;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context, UserManager<UserEntity> userManager)
    {
        AddRolesAndPermissions(context);

        await AddUsersAndMembershipsAsync(context, userManager);

        context.SaveChanges();
    }

    public static void AddRolesAndPermissions(AppDbContext context)
    {
        if (context.Roles.Any())
            return;

        var permissions = new List<PermissionEntity>
        {
            new() { Id = 1, Name = AppPermissions.ProjectView.Name.Value },
            new() { Id = 2, Name = AppPermissions.ProjectUpdate.Name.Value },
            new() { Id = 3, Name = AppPermissions.ProjectDelete.Name.Value },
            new() { Id = 4, Name = AppPermissions.ProjectAssignRole.Name.Value }
        };

        var roles = new List<RoleEntity>
        {
            new() { Id = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"), Name = AppRoles.ProjectOwner },
            new() { Id = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"), Name = AppRoles.ProjectContributor },
            new() { Id = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333"), Name = AppRoles.ProjectViewer },
        };

        var rolePermissions = new List<RolePermissionEntity>
        {
            // Owner
            new() { RoleId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"), PermissionId = 1 },
            new() { RoleId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"), PermissionId = 2 },
            new() { RoleId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"), PermissionId = 3 },
            new() { RoleId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"), PermissionId = 4 },

            // Contributor
            new() { RoleId = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"), PermissionId = 1 },
            new() { RoleId = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"), PermissionId = 2 },
            new() { RoleId = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"), PermissionId = 3 },

            // Viewer
            new() { RoleId = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333"), PermissionId = 1 },
        };

        context.Permissions.AddRange(permissions);
        context.Roles.AddRange(roles);
        context.RolePermissions.AddRange(rolePermissions);
    }

    public static async Task AddUsersAndMembershipsAsync(
        AppDbContext context,
        UserManager<UserEntity> userManager)
    {
        if (context.Users.Any())
            return;

        var user1 = new UserEntity
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            UserName = "owner@test.com",
            Email = "owner@test.com",
            NormalizedUserName = "OWNER@TEST.COM"
        };

        var user2 = new UserEntity
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            UserName = "contributor@test.com",
            Email = "contributor@test.com",
            NormalizedUserName = "CONTRIBUTOR@TEST.COM"
        };

        var user3 = new UserEntity
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            UserName = "viewer@test.com",
            Email = "viewer@test.com",
            NormalizedUserName = "VIEWER@TEST.COM"
        };

        await userManager.CreateAsync(user1, "Password123!");
        await userManager.CreateAsync(user2, "Password123!");
        await userManager.CreateAsync(user3, "Password123!");

        var project = new ProjectEntity
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Name = "Demo Project"
        };

        context.Projects.Add(project);

        var memberships = new List<ProjectMembershipEntity>
        {
            new() { ProjectId = project.Id, UserId = user1.Id, RoleId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111") },
            new() { ProjectId = project.Id, UserId = user2.Id, RoleId = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222") },
            new() { ProjectId = project.Id, UserId = user3.Id, RoleId = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333") }
        };

        context.ProjectMemberships.AddRange(memberships);
    }
}