using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recipe.Core.DTOs.Base;

namespace Recipe.API.Filters
{
    // public class ValidateFilterAttribute : ActionFilterAttribute
    // {
    //     public override void OnActionExecuting(ActionExecutingContext context)
    //     {
    //         if (!context.ModelState.IsValid)
    //         {
    //             var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
    //
    //             context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
    //         }
    //     }
    // }

    public class ValidateFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}