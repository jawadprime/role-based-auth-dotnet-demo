using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.DbEntities.Projects;
using Infrastructure.Persistence.DbEntities.Roles;
using Infrastructure.Persistence.DbEntities.Users;

namespace Infrastructure.Persistence.DbEntities.ProjectMember;
public class ProjectMembershipEntity : BaseEntity
{
    public Guid ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;

    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public Guid RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;
}