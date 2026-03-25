using Common.Errors;
using Common.Results;

namespace Domain.Validators;

public record ProjectArguments(ProjectId Id, ProjectName Name) 
{
    public Project ToDomain()
        => new(Id, Name);
};

public static class ProjectValidator
{
    public static Result Validate(ProjectArguments argument)
    {
        var failures = new List<string>();

        if (argument.Name.Value.Length > 100)
            failures.Add("Project name cannot exceed 100 characters.");

        if(failures.Any())
            return Result.Failure(new ValidationError(failures, new NoException()));

        return Result.Success();
    }
}