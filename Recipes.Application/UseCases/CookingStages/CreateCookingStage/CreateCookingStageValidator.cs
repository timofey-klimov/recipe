using FluentValidation;

namespace Recipes.Application.UseCases.CookingStages.CreateCookingStage
{
    public class CreateCookingStageValidator : AbstractValidator<CreateCookingStageCommand>
    {
        public CreateCookingStageValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithErrorCode("description")
                .WithMessage("Description should not be null");

            RuleFor(x => x.Image)
                .Must(x => x.Length > 0)
                .When(x => x.Image != null)
                .WithMessage("Image should not be empty")
                .WithErrorCode("image");
        }
    }
}
