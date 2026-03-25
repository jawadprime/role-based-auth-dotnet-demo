using Application.Abstractions;
using Common.Results;
using Domain;
using Domain.Constants;

namespace Application.Orchestrators;

public interface ProjectOrchestrator 
{
    public Task<Result<Project>> Update(Project project, UserId userId, CancellationToken cancellationToken);
}

public class ProjectOrchestratorImpl : ProjectOrchestrator
{
    private readonly AuthorizationService<ProjectId> _authService;
    private readonly ProjectRepo _projectRepo;

    public ProjectOrchestratorImpl(ProjectRepo projectRepo, AuthorizationService<ProjectId> authService)
    {
        _projectRepo = projectRepo;
        _authService = authService;
    }

    private readonly IReadOnlyList<Permission> _requiredPermissions = [AppPermissions.ProjectUpdate];

    public async Task<Result<Project>> Update(Project project, UserId userId, CancellationToken cancellationToken)
    {
        var isAuthorizedResult = await _authService.HasPermission(userId, project.Id, _requiredPermissions, cancellationToken);
        if (isAuthorizedResult.IsFailure) 
            return new(isAuthorizedResult.Error);

        var UpdateProjectResult = await _projectRepo.Update(project, cancellationToken);

        return UpdateProjectResult;
    }
}