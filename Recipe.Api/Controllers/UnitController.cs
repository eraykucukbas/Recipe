using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.DTOs.Unit;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Services;


namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : CustomBaseController
    {
        private readonly IUnitService _service;

        public UnitController(UserManager<UserApp> userManager, IUnitService service)
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
        public async Task<IActionResult> Save(UnitCreateDto unitCreateDto)
        {
            return CreateActionResult(await _service.Create(unitCreateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Save(UnitUpdateDto unitUpdateDto)
        {
            return CreateActionResult(await _service.Update(unitUpdateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }
    }
}