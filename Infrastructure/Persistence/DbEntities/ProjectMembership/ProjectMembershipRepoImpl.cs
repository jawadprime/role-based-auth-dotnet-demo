using Application.Abstractions;
using Common.Errors;
using Common.Results;
using Domain;
using Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DbEntities.ProjectMember;

public class ProjectMembershipRepoImpl : MembershipRepo<ProjectId>
{
    private readonly Repository<ProjectMembershipEntity> _repo;

    public ProjectMembershipRepoImpl(Repository<ProjectMembershipEntity> repo)
    {
        _repo = repo;
    }

    public async Task<Result<Role>> GetMemberRole(UserId userId, ProjectId resourceId, CancellationToken cancellationToken)
    {
        HasProjectId projectId = (HasProjectId)resourceId;
        var projectMemberEntity = await _repo.Find(pu => pu.UserId == userId.Value && pu.ProjectId == projectId.Value, cancellationToken)
            .Include(x => x.Role)
            .ThenInclude(x => x.RolePermissions)
            .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(cancellationToken);

        if (projectMemberEntity == null) 
        {
            var notFoundError = new NotFoundError([$"Project member not found. ProjectId: {projectId.Value}, UserId: {userId.Value}"], new NoException());
            return new(notFoundError);
        }

        var memberRole = projectMemberEntity.Role.ToDomain();
        return new(memberRole);
    }
}