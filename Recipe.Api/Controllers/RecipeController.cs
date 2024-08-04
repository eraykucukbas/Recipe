using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Services;


namespace Recipe.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : CustomBaseController
    {
        private readonly IRecipeService _service;
        // private readonly IMapper _mapper;

        public RecipeController(UserManager<UserApp> userManager, IRecipeService service)
            : base(userManager)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetById(id));
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(RecipeCreateDto recipeCreateDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Create(recipeCreateDto, activeUser));
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