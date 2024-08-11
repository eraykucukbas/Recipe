using System.Text.Json;
using FluentValidation;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.Unit;

namespace Recipe.Core.Validators
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}