using Common.Results;
using Domain;

namespace Application.Abstractions;

public interface MembershipRepo<TResourceId> where TResourceId : ResourceId
{
    Task<Result<Role>> GetMemberRole(UserId userId, TResourceId resourceId, CancellationToken cancellationToken);
}