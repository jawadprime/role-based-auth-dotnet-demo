namespace Domain;

public record UserId(Guid Value);

public abstract record PermissionId();
public record HasPermissionId(int Value): PermissionId;
public record NoPermissionId(): PermissionId;
public record PermissionName(string Value);

public abstract record ResourceId;
public record BoardId(Guid Value) : ResourceId; // Example: extend authorization for multiple resources with separate IDs
public record WorkItemId(Guid Value) : ResourceId; // Example: extend authorization for multiple resources with separate IDs
public abstract record ProjectId: ResourceId;
public record HasProjectId(Guid Value): ProjectId;
public record NoProjectId: ProjectId;
public record ProjectName(string Value);

public record RoleId(Guid Value);
public record RoleName(string Value);