using FluentValidation;
using Recipe.Core.DTOs.Favorite;

namespace Recipe.Core.Validators
{
    public class FavoriteCreateDtoValidator : AbstractValidator<FavoriteCreateDto>
    {
        public FavoriteCreateDtoValidator()
        {
            RuleFor(x => x.RecipeId)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number");
        }
    }
}