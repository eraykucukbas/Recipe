using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Services;


namespace Recipe.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : CustomBaseController
    {
        private readonly IFavoriteService _service;
        // private readonly IMapper _mapper;

        public FavoriteController(UserManager<UserApp> userManager, IFavoriteService service)
            : base(userManager)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] FilterReportDto filtersDto)
        {
            return CreateActionResult(await _service.GetAllAsync(filtersDto));
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMy([FromHeader] FilterPaginationDto filterPaginationDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.GetMyAsync(filterPaginationDto, activeUser));
        }

        [HttpPost]
        public async Task<IActionResult> Save(FavoriteCreateDto favoriteCreateDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Create(favoriteCreateDto, activeUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.RemoveAsync(id, activeUser));
        }
    }
}