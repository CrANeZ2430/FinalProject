using MediatR;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Application.Domain.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(
    IUsersRepository usersRepository) 
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(
        GetUserByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetById(query.UserId, cancellationToken);

        return new UserDto(
            user.UserId,
            user.UserName,
            user.Email,
            user.ProfilePicturePath,
            user.Bio);
    }
}