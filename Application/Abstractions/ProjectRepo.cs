using Common.Results;
using Domain;

namespace Application.Abstractions;

public interface ProjectRepo
{
    Task<Result<Project>> GetById(ProjectId id, CancellationToken cancellationToken);
    Task<Result<Project>> Update(Project project, CancellationToken cancellationToken);
}