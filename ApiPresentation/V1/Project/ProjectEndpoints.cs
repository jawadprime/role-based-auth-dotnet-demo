using Application.Orchestrators;
using Domain;
using MinimalApi.Extensions;
using MinimalApi.V1.Projects.Contracts;

namespace MinimalApi.V1.Projects;
public static class ProjectEndpoints
{
    public static RouteGroupBuilder MapProjectEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/v1/projects")
            .WithTags("Projects");

        group.MapPost("/Update", UpdateProject)
            .WithName("UpdateProject")
            .Produces<AddProjectResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        return group;
    }

    private static async Task<IResult> UpdateProject(
    UpdateProjectRequest request,
    ProjectOrchestrator orchestrator,
    CancellationToken cancellationToken)
    {
        var projectToUpdateResult = Project.Create(request.ToDomainArguments());

        if (projectToUpdateResult.IsFailure)
            return projectToUpdateResult.ToProblemResult();

        var projectToUpdate = projectToUpdateResult.Value;
        var userId = new UserId(request.UserId);
        var result = await orchestrator.Update(projectToUpdate, userId, cancellationToken);

        var apiResponse = result.ToActionResult(AddProjectResponse.FromDomain);
        return apiResponse;
    }
}