using Microsoft.AspNetCore.Identity;
using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
using Recipe.Core.Exceptions;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
using Recipe.Infrastructure.Mappings;

namespace Recipe.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;

        private readonly IUserService _userService;

        // private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(ITokenService tokenService,
            UserManager<UserApp> userManager, IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<CustomResponseDto<UserAppDto>> RegisterAsync(UserCreateDto userCreateDto)
        {
            var existingUser = await _userManager.FindByNameAsync(userCreateDto.UserName);
            if (existingUser != null)
            {
                throw new AlreadyDefinedException("username already defined");
            }
            var user = await _userService.CreateUserAsync(userCreateDto);
            return CustomResponseDto<UserAppDto>.Success(200, user.Data);
        }
        
        public async Task<CustomResponseDto<UserAppDto>> RegisterAdminAsync(UserCreateDto userCreateDto)
        {
            var existingUser = await _userManager.FindByNameAsync(userCreateDto.UserName);
            if (existingUser != null) {
                throw new AlreadyDefinedException("Username already defined");
            }
            var user = await _userService.CreateAdminUserAsync(userCreateDto);
            return CustomResponseDto<UserAppDto>.Success(200, user.Data);
        }

        public async Task<CustomResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.CheckCredentials(loginDto);
            if (!user.IsActive)
            {
                throw new ForbiddenException("Passive Account");
            }
            var token = await _tokenService.CreateToken(user);
            var authResponseDto = AuthMapper.ToDto(token);
            if (authResponseDto is null)
            {
                throw new ServerException("Failed map TokenDto to AuthResponseDto!");
            }

            var userToken = new IdentityUserToken<string>
            {
                UserId = user.Id,
                LoginProvider = "Recipe",
                Name = "RefreshToken",
                Value = authResponseDto.RefreshToken
            };

            await _userManager.SetAuthenticationTokenAsync(user, userToken.LoginProvider, userToken.Name,
                userToken.Value);

            return CustomResponseDto<AuthResponseDto>.Success(200, authResponseDto);
        }
        
        public async Task<CustomResponseDto<NoContentDto>> ActiveOrPassiveTrigger(ActiveOrPassviceTriggerDto activeOrPassiveTriggerDto)
        {
            var user = await _userManager.FindByIdAsync(activeOrPassiveTriggerDto.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);
            return CustomResponseDto<NoContentDto>.Success(200);
        } 
        
    }
}