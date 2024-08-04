using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Token;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Mappings
{
    public static class AuthMapper
    {
        public static AuthResponseDto? ToDto(TokenDto? dto)
        {
            if (dto is null) return null;

            return new AuthResponseDto
            {
                AccessToken = dto.AccessToken,
                RefreshToken = dto.RefreshToken
            };
        }

        // public static UserApp? ToEntity(UserAppDto? userAppDto)
        // {
        //     if (userAppDto is null) return null;
        //
        //     return new UserApp
        //     {
        //         Id = userAppDto.Id,
        //         UserName = userAppDto.UserName,
        //         Name = userAppDto.Name,
        //         Surname = userAppDto.Surname,
        //         Email = userAppDto.Email
        //     };
        // }
    }
}