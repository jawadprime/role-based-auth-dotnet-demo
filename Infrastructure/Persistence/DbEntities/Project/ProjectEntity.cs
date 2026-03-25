using Common.Results;
using Domain;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.DbEntities.Projects;
public class ProjectEntity : BaseEntity<Guid>
{
    public required string Name { get; set; }

    public Result<Domain.Project> ToDomain() 
        => Domain.Project.Create(new(new HasProjectId(Id), new(Name)));

    public static ProjectEntity FromDomain(Domain.Project domain)
        => new ProjectEntity { Id = ((HasProjectId)domain.Id).Value, Name = domain.Name.Value };
}