namespace Domain;

public record Role(RoleId Id, RoleName Name, IReadOnlyList<Permission> Permissions);