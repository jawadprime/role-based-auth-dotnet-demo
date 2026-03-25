using Infrastructure.Persistence.DbEntities.Permissions;
using Infrastructure.Persistence.DbEntities.ProjectMember;
using Infrastructure.Persistence.DbEntities.Projects;
using Infrastructure.Persistence.DbEntities.RolePermissions;
using Infrastructure.Persistence.DbEntities.Roles;
using Infrastructure.Persistence.DbEntities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    internal DbSet<PermissionEntity> Permissions => Set<PermissionEntity>();
    internal DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();
    internal DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    internal DbSet<ProjectMembershipEntity> ProjectMemberships => Set<ProjectMembershipEntity>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}