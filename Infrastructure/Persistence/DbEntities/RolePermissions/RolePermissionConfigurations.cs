namespace Infrastructure.Persistence.DbEntities.RolePermissions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.HasIndex(rp => rp.RoleId);

        builder.HasOne(rp => rp.Role)
               .WithMany(r => r.RolePermissions)
               .HasForeignKey(rp => rp.RoleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rp => rp.Permission)
               .WithMany(p => p.RolePermissions)
               .HasForeignKey(rp => rp.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}