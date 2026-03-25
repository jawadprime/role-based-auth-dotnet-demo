namespace Common.Errors;

public record NotFoundError(IReadOnlyList<string> Failures, MaybeException MaybeException)
    : HasError("Resource Not Found", Failures, MaybeException);

public record ValidationError(IReadOnlyList<string> Failures, MaybeException MaybeException)
    : HasError("Validation Failed", Failures, MaybeException);

public record AuthorizationError(IReadOnlyList<string> Failures, MaybeException MaybeException)
    : HasError("Authorization Failed", Failures, MaybeException);

public record UnexpectedError(IReadOnlyList<string> Failures, MaybeException MaybeException)
    : HasError("Unexpected Error", Failures, MaybeException);