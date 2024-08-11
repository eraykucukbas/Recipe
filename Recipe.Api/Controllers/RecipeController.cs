using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.DTOs.Ingredient;
using Recipe.Core.DTOs.Instruction;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.Tag;
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
        public async Task<IActionResult> GetAllAsync([FromHeader] FilterRecipeDto filterRecipeDto)
        {
            return CreateActionResult(await _service.GetAllAsync(filterRecipeDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetById(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] RecipeCreateFormDataDto recipeCreateFormDataDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Create(recipeCreateFormDataDto, activeUser));
        }
        
        
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] RecipeUpdateFormDataDto recipeUpdateFormDataDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Update(recipeUpdateFormDataDto, activeUser));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Remove(id, activeUser));
        }
    }
}