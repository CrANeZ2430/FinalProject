using MediatR;

public record UserExistsQuery(
    string UserId)
    : IRequest<bool>;
