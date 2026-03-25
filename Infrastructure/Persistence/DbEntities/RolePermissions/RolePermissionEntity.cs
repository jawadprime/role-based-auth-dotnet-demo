using Domain;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.DbEntities.Permissions;
using Infrastructure.Persistence.DbEntities.Roles;

namespace Infrastructure.Persistence.DbEntities.RolePermissions;

public class RolePermissionEntity : BaseEntity
{
    public Guid RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    public int PermissionId { get; set; }
    public PermissionEntity Permission { get; set; } = null!;

    public Permission ToPermissionDomain()
        => Permission.ToDomain(); 
}