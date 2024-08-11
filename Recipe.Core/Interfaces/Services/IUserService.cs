using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<CustomResponseDto<UserAppDto>> CreateUserAsync(UserCreateDto userCreateDto);
        Task<CustomResponseDto<UserAppDto>> CreateAdminUserAsync(UserCreateDto userCreateDto);
        Task<CustomResponseDto<NoContentDto>> UpdateUserAsync(string username,  UserUpdateDto userUpdateDto);
        Task<CustomResponseDto<NoContentDto>> ChangePassword(string username,  UserChangePasswordDto userChangePasswordDto);
        Task<CustomResponseDto<UserAppDto>> GetMyUser(string username);
        Task<UserApp> CheckCredentials(LoginDto loginDto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(string username);

    }
}