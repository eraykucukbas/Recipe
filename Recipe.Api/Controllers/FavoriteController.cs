using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Recipe;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _service.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Save(FavoriteCreateDto favoriteCreateDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Create(favoriteCreateDto, activeUser));
        }
        //
        // [HttpPut]
        // public async Task<IActionResult> Update(TodoListUpdateDto todoListUpdateDto)
        // {
        //     return await ExecuteServiceAsync(async activeUser =>
        //         await _service.UpdateTodoList(todoListUpdateDto, activeUser));
        // }
        //
        // [HttpPut("isCompletedTrigger")]
        // public async Task<IActionResult> IsCompletedTrigger([FromBody] IsCompletedTriggerDto dto)
        // {
        //     return await ExecuteServiceAsync(async activeUser =>
        //         await _service.IsCompletedTrigger(dto.Id, activeUser));
        // }
        //
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Remove(int id)
        // {
        //     return await ExecuteServiceAsync(async activeUser =>
        //         await _service.RemoveTodoList(id, activeUser));
        // }
    }
}