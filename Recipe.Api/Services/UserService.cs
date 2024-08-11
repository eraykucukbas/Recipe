using Microsoft.AspNetCore.Identity;
using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
using Recipe.Core.Exceptions;
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
                ? throw new NotFoundException("User not found")
                : CustomResponseDto<UserApp>.Success(200, user);
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = new UserApp
            {
                Email = userCreateDto.Email,
                UserName = userCreateDto.UserName,
                Name = userCreateDto.Name,
                Surname = userCreateDto.Surname,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            if (!result.Succeeded)
            {
                throw new ServerException();
            }

            var userAppDto = UserMapper.ToDto(user);
            if (userAppDto is null)
            {
                throw new ServerException("UserMapper Error");
            }

            await _userManager.AddToRoleAsync(user, "User");

            return CustomResponseDto<UserAppDto>.Success(200, userAppDto);
        }
        
        public async Task<CustomResponseDto<UserAppDto>> CreateAdminUserAsync(UserCreateDto userCreateDto)
        {
            var user = new UserApp
            {
                Email = userCreateDto.Email,
                UserName = userCreateDto.UserName,
                Name = userCreateDto.Name,
                Surname = userCreateDto.Surname,
                IsActive = false
            };

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            if (!result.Succeeded)
            {
                throw new ServerException();
            }

            var userAppDto = UserMapper.ToDto(user);
            if (userAppDto is null)
            {
                throw new ServerException("UserMapper Error");
            }

            await _userManager.AddToRoleAsync(user, "Admin");
            return CustomResponseDto<UserAppDto>.Success(200, userAppDto);
        }

       
        public async Task<CustomResponseDto<NoContentDto>> UpdateUserAsync(string username, UserUpdateDto userUpdateDto)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            var user = userResponse.Data;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                throw new ServerException(errors.ToString());
            }
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> ChangePassword(string username,
            UserChangePasswordDto userChangePasswordDto)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            var user = userResponse.Data;
            var result = await _userManager.ChangePasswordAsync(user, userChangePasswordDto.OldPassword,
                userChangePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                throw new ServerException(errors.ToString());
            }
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetMyUser(string username)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            var userAppDto = UserMapper.ToDto(userResponse.Data);
            if (userAppDto is null)
            {
                throw new NotFoundException("User not found");
            }
            return CustomResponseDto<UserAppDto>.Success(200, userAppDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(string username)
        {
            var userResponse = await GetUserByUsernameAsync(username);
            var user = userResponse.Data;
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                throw new ServerException(errors.ToString());
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<UserApp> CheckCredentials(LoginDto loginDto)
        {
            var userResponse = await GetUserByUsernameAsync(loginDto.UserName);
            var user = userResponse.Data;
            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValid)
            {
                throw new BadHttpRequestException("Wrong credentials");
            }
            return user;
        }
    }
}