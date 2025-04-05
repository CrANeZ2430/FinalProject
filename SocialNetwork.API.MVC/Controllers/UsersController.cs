using Auth0.AspNetCore.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models;
using SocialNetwork.Application.Domain.Users.Commands.CreateUser;
using SocialNetwork.Application.Domain.Users.Commands.DeleteUser;
using System.Security.Claims;

namespace SocialNetwork.API.MVC.Controllers;

public class UsersController(
    IMediator mediator) 
    : Controller
{
    public async Task Login(string returnUrl = "/", CancellationToken cancellationToken = default)
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(Url.Action("AddUser", "Users"))
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }

    public async Task Signup(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithParameter("screen_hint", "signup")
            .WithRedirectUri(returnUrl)
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }

    [Authorize]
    public async Task Logout()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(Url.Action("Index", "Home"))
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [Authorize]
    public async Task<IActionResult> UserProfile(
        CancellationToken cancellationToken = default)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userName = User.Identity.Name;
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var profilePicture = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

        var userProfile = new UserProfile(
            userId,
            userName,
            email,
            profilePicture);

        return View(userProfile);
    }

    [Authorize]
    public async Task<IActionResult> AddUser(
        CancellationToken cancellationToken = default)
    {
        var userExistsQuery = new UserExistsQuery(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        var userExists = await mediator.Send(userExistsQuery, cancellationToken);

        if (!userExists)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = User.Identity.Name;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var profilePicture = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            var command = new CreateUserCommand(
                userId,
                userName,
                email,
                profilePicture,
                null);

            await mediator.Send(command, cancellationToken);
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task DeleteUser(
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteUserCommand(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        await mediator.Send(command, cancellationToken);

        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(Url.Action("Index", "Home"))
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
