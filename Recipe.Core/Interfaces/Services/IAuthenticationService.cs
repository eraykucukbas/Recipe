using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.User;

namespace Recipe.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<UserAppDto>> RegisterAsync(UserCreateDto userCreateDto);
        Task<CustomResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto);

    }
}