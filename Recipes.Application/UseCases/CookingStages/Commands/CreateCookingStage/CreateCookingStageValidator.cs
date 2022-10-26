using FluentValidation;

namespace Recipes.Application.UseCases.CookingStages.Commands.CreateCookingStage
{
    public class CreateCookingStageValidator : AbstractValidator<CreateCookingStageCommand>
    {
        public CreateCookingStageValidator()
        {
            RuleFor(x => x.Stage)
                .NotNull()
                .WithErrorCode("stage")
                .WithMessage("Cooking stage should not be null");

            RuleFor(x => x.Stage.Description)
                .NotEmpty()
                .WithErrorCode("description")
                .WithMessage("Description should not be null");


            RuleFor(x => x.Stage.Image.Length)
                .GreaterThan(0)
                .When(x => x.Stage.Image != null)
                .WithErrorCode("image")
                .WithMessage("Image should not be empty");
        }
    }
}
