﻿using MediatR;
using SocialNetwork.Application.Common;

namespace SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;

public record GetUserCommentsQuery(
    Guid UserId,
    int Page,
    int PageSize)
    : IRequest<PageResponse<UserDto>>;
