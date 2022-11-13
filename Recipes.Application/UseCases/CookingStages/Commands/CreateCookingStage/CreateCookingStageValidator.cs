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

        }
    }
}
