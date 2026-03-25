using Application.Abstractions;
using Common.Errors;
using Common.Results;
using Domain;

namespace Infrastructure.Services;

public class AuthorizationServiceImpl<TResourceId> : AuthorizationService<TResourceId> where TResourceId : ResourceId
{
    private readonly MembershipRepo<TResourceId> _membershipRepo;

    public AuthorizationServiceImpl(MembershipRepo<TResourceId> membershipRepo)
    {
        _membershipRepo = membershipRepo;
    }

    public async Task<Result<bool>> HasPermission(UserId userId, TResourceId resourceId, IReadOnlyList<Permission> requiredPermissions, CancellationToken cancellationToken)
    {
        var getMemberRoleResult = await _membershipRepo.GetMemberRole(userId, resourceId, cancellationToken);

        if (getMemberRoleResult.IsFailure)
            return new(getMemberRoleResult.Error);

        var memberRole = getMemberRoleResult.Value;
        var hasRequiredPermissionsResult = DoesRoleHaveRequiredPermissions(memberRole, requiredPermissions);
        
        return hasRequiredPermissionsResult;
    }

    public Result<bool> DoesRoleHaveRequiredPermissions(
        Role role,
        IReadOnlyList<Permission> requiredPermissions)
    {

        var hasRequiredPermissions = requiredPermissions.All(requiredPermission =>
            role.Permissions.Any(rolePermission =>
                string.Equals(
                    rolePermission.Name.Value,
                    requiredPermission.Name.Value,
                    StringComparison.OrdinalIgnoreCase)));

        if (!hasRequiredPermissions)
            return new(new AuthorizationError(["User doesn't have required permissions to perform this action."] ,new NoException()));

        return new(true);
    }
}