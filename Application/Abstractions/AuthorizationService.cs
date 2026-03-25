using Common.Results;
using Domain;

namespace Application.Abstractions;

public interface AuthorizationService<TResourceId> where TResourceId : ResourceId
{
    Task<Result<bool>> HasPermission(
        UserId userId,
        TResourceId resourceId,
        IReadOnlyList<Permission> permissions,
        CancellationToken cancellationToken
    );
}