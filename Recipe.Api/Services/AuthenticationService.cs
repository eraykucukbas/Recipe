using Microsoft.AspNetCore.Identity;
using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
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
            var user = await _userService.CreateUserAsync(userCreateDto);
            return CustomResponseDto<UserAppDto>.Success(200, user.Data);
        }

        public async Task<CustomResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.CheckCredentials(loginDto);
            if (user.StatusCode == 404)
            {
                return CustomResponseDto<AuthResponseDto>.Fail(404, "user not found");
            }

            var token = await _tokenService.CreateToken(user.Data);
            // tokendto to AuthResponseDto
            // var tokenDto = _mapper.Map<AuthResponseDto>(token);

            var authResponseDto = AuthMapper.ToDto(token);

            if (authResponseDto is null)
            {
                return CustomResponseDto<AuthResponseDto>.Fail(400, "Failed map TokenDto to AuthResponseDto!");
            }

            var userToken = new IdentityUserToken<string>
            {
                UserId = user.Data.Id,
                LoginProvider = "Recipe",
                Name = "RefreshToken",
                Value = authResponseDto.RefreshToken
            };

            await _userManager.SetAuthenticationTokenAsync(user.Data, userToken.LoginProvider, userToken.Name,
                userToken.Value);

            return CustomResponseDto<AuthResponseDto>.Success(200, authResponseDto);
        }
    }
}