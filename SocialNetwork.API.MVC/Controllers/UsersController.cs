using Auth0.AspNetCore.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models;
using SocialNetwork.Application.Domain.Users.Commands.CreateUser;
using System.Security.Claims;
using System.Threading;

namespace SocialNetwork.API.MVC.Controllers;

public class UsersController(
    IMediator mediator) 
    : Controller
{
    public async Task Login(string returnUrl = "/", CancellationToken cancellationToken = default)
    {
        //if (existingUser == null)
        //{
        //    var createUserCommand = new CreateUserCommand(userId, userName, email, profilePicture);
        //    await _mediator.Send(createUserCommand, cancellationToken);
        //}

        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            // Indicate here where Auth0 should redirect the user after a login.
            // Note that the resulting absolute Uri must be added to the
            // **Allowed Callback URLs** settings for the app.
            .WithRedirectUri(returnUrl)
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
            // Indicate here where Auth0 should redirect the user after a logout.
            // Note that the resulting absolute Uri must be added to the
            // **Allowed Logout URLs** settings for the app.
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

        var userProfile = new UserProfile(
            userId,
            User.Identity.Name,
            User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
            User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value);

        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"Claim type: {claim.Type}");
        }

        //var userName = User.Identity.Name;
        //var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        //var profilePicture = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

        //var command = new CreateUserCommand(
        //    userId,
        //    userName,
        //    email,
        //    profilePicture,
        //    null);

        //await mediator.Send(command, cancellationToken);

        return View(userProfile);
    }
}
