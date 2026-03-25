using Domain;
using Infrastructure.Persistence.DbEntities.RolePermissions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.DbEntities.Roles;

public class RoleEntity : IdentityRole<Guid>
{
    public ICollection<RolePermissionEntity> RolePermissions { get; set; } = new List<RolePermissionEntity>();

    public Role ToDomain()
        => new Role(new(Id), new(Name), RolePermissions.Select(rp => rp.ToPermissionDomain()).ToList()); 
}