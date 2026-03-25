namespace Infrastructure.Persistence.DbEntities.Permissions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.ToTable("PermissionsEntity");

        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.RolePermissions)
               .WithOne(rp => rp.Permission)
               .HasForeignKey(rp => rp.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}