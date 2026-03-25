using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbEntities.ProjectMember;

internal class ProjectMembershipConfiguration : IEntityTypeConfiguration<ProjectMembershipEntity>
{
    public void Configure(EntityTypeBuilder<ProjectMembershipEntity> builder)
    {
        builder.ToTable("project_memberships");

        builder.HasKey(pu => new { pu.UserId, pu.ProjectId });

        builder
         .HasOne(pu => pu.Project)
         .WithMany()
         .HasForeignKey(pu => pu.ProjectId)
         .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pu => pu.User)
            .WithMany(u => u.ProjectUsers)
            .HasForeignKey(pu => pu.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pu => pu.Role)
            .WithMany()
            .HasForeignKey(pu => pu.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}