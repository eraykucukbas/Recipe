using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
using IAuthenticationService = Recipe.Core.Interfaces.Services.IAuthenticationService;

namespace Recipe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService, UserManager<UserApp> userManager)
            : base(userManager)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            return CreateActionResult(await _authenticationService.RegisterAsync(userCreateDto));
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin(UserCreateDto userCreateDto)
        {
            return CreateActionResult(await _authenticationService.RegisterAdminAsync(userCreateDto));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return CreateActionResult(await _authenticationService.LoginAsync(loginDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("activeOrPassiveTrigger")]
        public async Task<IActionResult> ActiveOrPassiveTrigger(ActiveOrPassviceTriggerDto activeOrPassiveTriggerDto)
        {
            return CreateActionResult(await _authenticationService.ActiveOrPassiveTrigger(activeOrPassiveTriggerDto));
        }
    }
}