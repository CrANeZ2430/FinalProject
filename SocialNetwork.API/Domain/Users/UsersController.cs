﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Common.Constants;
using SocialNetwork.API.Domain.Users.Records;
using SocialNetwork.Application.Domain.Users.Commands.CreateUser;
using SocialNetwork.Application.Domain.Users.Queries.GetUsers;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Domain.Users;

[Route(Routes.Users)]
public class UsersController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers(
        [FromQuery][Required] int page = 1,
        [FromQuery][Required] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUsersQuery(
            page,
            pageSize);

        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromQuery] CreateUserRequest request,
        CancellationToken cancellation = default)
    {
        var command = new CreateUserCommand(
            request.UserName,
            request.Email,
            request.Password,
            request.ProfilePicturePath,
            request.Bio);

        var id = await mediator.Send(command, default);
        return Ok(id);
    }
}
