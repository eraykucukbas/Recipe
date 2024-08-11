using System.Text.Json;
using FluentValidation;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.Unit;

namespace Recipe.Core.Validators
{
    public class UnitCreateDtoValidator : AbstractValidator<UnitCreateDto>
    {
        public UnitCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}