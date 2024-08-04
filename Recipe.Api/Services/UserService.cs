using Microsoft.AspNetCore.Identity;
using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Services;
using Recipe.Infrastructure.Mappings;

namespace Recipe.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        private async Task<CustomResponseDto<UserApp>> GetUserByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user == null
                ? CustomResponseDto<UserApp>.Fail(404, "User not found")
                : CustomResponseDto<UserApp>.Success(200, user);
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = new UserApp
            {
                Email = userCreateDto.Email,
                UserName = userCreateDto.UserName,
                Name = userCreateDto.Name,
                Surname = userCreateDto.Surname
            };

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<UserAppDto>.Fail(400, errors);
            }

            var userAppDto = UserMapper.ToDto(user);
            if (userAppDto is null)
            {
                return CustomResponseDto<UserAppDto>.Fail(400, "Map error userAppDto");
            }

            return CustomResponseDto<UserAppDto>.Success(200, userAppDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateUserAsync(string username, UserUpdateDto userUpdateDto)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            if (userResponse.StatusCode == 404)
            {
                return CustomResponseDto<NoContentDto>.Fail(userResponse.StatusCode, userResponse.Errors);
            }

            var user = userResponse.Data;
            // _mapper.Map(userUpdateDto, user);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(400, errors);
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> ChangePassword(string username,
            UserChangePasswordDto userChangePasswordDto)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            if (userResponse.StatusCode == 404)
            {
                return CustomResponseDto<NoContentDto>.Fail(userResponse.StatusCode, userResponse.Errors);
            }

            var user = userResponse.Data;
            var result = await _userManager.ChangePasswordAsync(user, userChangePasswordDto.OldPassword,
                userChangePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(400, errors);
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetMyUser(string username)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            if (userResponse.StatusCode == 404)
            {
                return CustomResponseDto<UserAppDto>.Fail(userResponse.StatusCode, userResponse.Errors);
            }

            // var userDto = _mapper.Map<UserAppDto>(userResponse.Data);

            var userAppDto = UserMapper.ToDto(userResponse.Data);
            if (userAppDto is null)
            {
                return CustomResponseDto<UserAppDto>.Fail(400, "Map error userAppDto");
            }

            return CustomResponseDto<UserAppDto>.Success(200, userAppDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(string username)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            if (userResponse.StatusCode == 404)
            {
                return CustomResponseDto<NoContentDto>.Fail(userResponse.StatusCode, userResponse.Errors);
            }

            var user = userResponse.Data;
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(400, errors);
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<UserApp>> CheckCredentials(LoginDto loginDto)
        {
            var userResponse = await GetUserByUsernameAsync(loginDto.UserName);
            if (userResponse.StatusCode == 404)
            {
                return CustomResponseDto<UserApp>.Fail(userResponse.StatusCode, userResponse.Errors);
            }

            var user = userResponse.Data;
            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValid)
            {
                return CustomResponseDto<UserApp>.Fail(400, "Wrong credentials!");
            }

            return CustomResponseDto<UserApp>.Success(200, user);
        }
    }
}