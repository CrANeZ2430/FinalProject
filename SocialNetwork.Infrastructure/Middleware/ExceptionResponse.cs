using System.Net;

namespace SocialNetwork.Infrastructure.Middleware;

public record ExceptionResponse(
    HttpStatusCode StatusCode,
    object Data);
