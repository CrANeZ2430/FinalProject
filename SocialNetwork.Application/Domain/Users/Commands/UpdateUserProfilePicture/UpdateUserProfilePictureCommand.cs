using MediatR;

namespace SocialNetwork.Application.Domain.Users.Commands.UpdateUserProfilePicture;

public record UpdateUserProfilePictureCommand(
    string UserId,
    string ProfilePicturePath)
    : IRequest;
