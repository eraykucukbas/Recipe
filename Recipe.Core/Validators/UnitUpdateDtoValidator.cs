using System.Text.Json;
using FluentValidation;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.Unit;

namespace Recipe.Core.Validators
{
    public class UnitUpdateDtoValidator : AbstractValidator<UnitUpdateDto>
    {
        public UnitUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number");
            
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}