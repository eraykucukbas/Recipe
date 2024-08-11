using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Comment;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Services;


namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : CustomBaseController
    {
        private readonly ICommentService _service;

        public CommentController(UserManager<UserApp> userManager, ICommentService service)
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

        [HttpGet("GetParentCommentsByRecipe/{recipeId}")]
        public async Task<IActionResult> GetParentCommentsByRecipe(int recipeId,
            [FromHeader] FilterPaginationDto filterPaginationDto)
        {
            return CreateActionResult(await _service.GetParentCommentsByRecipe(recipeId, filterPaginationDto));
        }
        
        [HttpGet("GetCommentsByParent/{parentComment}")]
        public async Task<IActionResult> GetCommentsByParent(int parentComment,
            [FromHeader] FilterPaginationDto filterPaginationDto)
        {
            return CreateActionResult(await _service.GetCommentsByParent(parentComment, filterPaginationDto));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(CommentCreateDto commentCreateDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Create(commentCreateDto, activeUser));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(CommentUpdateDto commentUpdateDto)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.Update(commentUpdateDto, activeUser));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return await ExecuteServiceAsync(async activeUser =>
                await _service.RemoveAsync(id, activeUser));
        }
    }
}