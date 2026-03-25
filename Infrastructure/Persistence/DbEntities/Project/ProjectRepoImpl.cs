using Application.Abstractions;
using Common.Errors;
using Common.Results;
using Domain;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.DbEntities.Projects;

namespace Infrastructure.Persistence.Repositories;

public class ProjectRepoImpl : ProjectRepo
{
    private readonly Repository<ProjectEntity> _repo;

    public ProjectRepoImpl(AppDbContext context)
    {
        _repo = new Repository<ProjectEntity>(context);
    }

    public async Task<Result<Project>> Update(Project project, CancellationToken cancellationToken)
    {
        try
        {
            var projectEntityToUpdate = ProjectEntity.FromDomain(project);

            await _repo.UpdateAsync(projectEntityToUpdate, cancellationToken);

            return new(project);
        }
        catch (Exception ex)
        {
            return new(
                new UnexpectedError([ex.Message], new HasException(ex))
            );
        }
    }

    public async Task<Result<Project>> GetById(ProjectId projectId, CancellationToken cancellationToken)
    {
        try
        {
            var foundProjectEntity = await _repo.FindOneAsync(p => p.Id == ((HasProjectId)projectId).Value, cancellationToken);

            if (foundProjectEntity == null)
            {
                var notFoundError = new NotFoundError([$"Project not found. ProjectId: {((HasProjectId)projectId).Value}"], new NoException());
                return new(notFoundError);
            }

            var foundProjectResult = foundProjectEntity.ToDomain();
            
            return foundProjectResult;
        }
        catch (Exception ex)
        {
            return new(
                new UnexpectedError([ex.Message], new HasException(ex))
            );
        }
    }
}