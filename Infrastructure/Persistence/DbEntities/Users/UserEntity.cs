using Infrastructure.Persistence.DbEntities.ProjectMember;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.DbEntities.Users;

public class UserEntity : IdentityUser<Guid>
{
    internal ICollection<ProjectMembershipEntity> ProjectUsers { get; set; } = new List<ProjectMembershipEntity>();
}