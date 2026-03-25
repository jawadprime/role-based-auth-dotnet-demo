using Common.Results;
using Domain.Validators;

namespace Domain;

public record Project(ProjectId Id, ProjectName Name) 
{
    public static Result<Project> Create(ProjectArguments arguments) 
    {
        var validationResult = ProjectValidator.Validate(arguments);

        if (validationResult.IsFailure)
            return new(validationResult.Error);

        var project = arguments.ToDomain();
        return new(project);
    }
}