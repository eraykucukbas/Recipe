using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Services;


namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _service;

        public CategoryController(UserManager<UserApp> userManager, ICategoryService service)
            : base(userManager)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] FilterPaginationDto filterPaginationDto)
        {
            return CreateActionResult(await _service.GetAllAsync(filterPaginationDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(CategoryCreateDto categoryCreateDto)
        {
            return CreateActionResult(await _service.Create(categoryCreateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Save(CategoryUpdateDto categoryUpdateDto)
        {
            return CreateActionResult(await _service.Update(categoryUpdateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }
    }
}