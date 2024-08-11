using System.Text.Json;
using FluentValidation;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Recipe;

namespace Recipe.Core.Validators
{
    public class RecipeUpdateDtoValidator : AbstractValidator<RecipeUpdateFormDataDto>
    {
        public RecipeUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number");

            RuleFor(x => x.Title)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("{PropertyName} cannot exceed 500 characters");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number");

            RuleFor(x => x.Ingredients)
                .Must(BeValidJsonArray).WithMessage("{PropertyName} must be a valid JSON array");

            RuleFor(x => x.Instructions)
                .Must(BeValidJsonArray).WithMessage("{PropertyName} must be a valid JSON array");

            RuleFor(x => x.Tags)
                .Must(BeValidJsonArray).WithMessage("{PropertyName} must be a valid JSON array");
        }

        private bool BeValidJsonArray(string? json)
        {
            if (string.IsNullOrEmpty(json))
                return false;

            try
            {
                var parsed = JsonSerializer.Deserialize<object[]>(json);
                return parsed != null;
            }
            catch
            {
                return false;
            }
        }
    }
}