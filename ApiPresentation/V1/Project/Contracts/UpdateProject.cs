using Domain;
using Domain.Validators;

namespace MinimalApi.V1.Projects.Contracts;

public record UpdateProjectRequest(Guid id, string Name, Guid UserId) 
{
    public ProjectArguments ToDomainArguments() 
    {
        return new(new HasProjectId(id), new(Name));
    }
}

public record AddProjectResponse(Guid Id, string Name)
{
    public static AddProjectResponse FromDomain(Project Project)
    {
        return new(((HasProjectId)Project.Id).Value, Project.Name.Value);
    }
}