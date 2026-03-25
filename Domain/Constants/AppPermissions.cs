namespace Domain.Constants;

public static class AppPermissions
{
    public static readonly Permission ProjectCreate = new(new("project.create"));
    public static readonly Permission ProjectView = new(new("project.view"));
    public static readonly Permission ProjectUpdate = new(new("project.update"));
    public static readonly Permission ProjectDelete = new(new("project.delete"));
    public static readonly Permission ProjectAssignRole = new(new("project.assign_role"));
}