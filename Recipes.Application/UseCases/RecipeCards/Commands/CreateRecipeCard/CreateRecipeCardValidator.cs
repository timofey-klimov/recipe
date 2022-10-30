using FluentValidation;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard
{
    public class CreateRecipeCardValidator : AbstractValidator<CreateRecipeCardCommand>
    {
        public CreateRecipeCardValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title should not be null")
                .WithErrorCode("title");

            RuleFor(x => x.Remark)
                .NotNull()
                .WithMessage("Remark should not be null")
                .WithErrorCode("remark");

            RuleForEach(x => x.Hashtags)
                .ChildRules(hashtag =>
                {
                    hashtag.RuleFor(x => x)
                        .NotEmpty()
                        .WithMessage("Hashtag should not be null")
                        .WithErrorCode("hashtag");
                })
                .When(x => x.Hashtags?.Any() == true);
        }
    }
}
