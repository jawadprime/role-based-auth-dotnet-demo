using Domain;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.DbEntities.RolePermissions;

namespace Infrastructure.Persistence.DbEntities.Permissions;

public class PermissionEntity : BaseEntity<int>
{
    public required string Name { get; set; }
    public ICollection<RolePermissionEntity> RolePermissions { get; set; } = new List<RolePermissionEntity>();

    public Permission ToDomain()
        => new(new(Name));
}