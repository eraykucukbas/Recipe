using FluentValidation;
using Recipe.Core.Entities;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(comment => comment.Message)
            .NotEmpty().WithMessage("Message is required")
            .MaximumLength(500).WithMessage("Message cannot exceed 500 characters");

        RuleFor(comment => comment.Rate)
            .NotNull().When(comment => comment.ParentComment == null)
            .WithMessage("Rate is required for parent comments")
            .Null().When(comment => comment.ParentComment != null)
            .WithMessage("Rate should not be provided for child comments");
    }
}