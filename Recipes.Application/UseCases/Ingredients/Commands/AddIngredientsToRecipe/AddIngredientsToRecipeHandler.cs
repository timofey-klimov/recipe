using MediatR;
using Recipes.Contracts.Ingredients;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.Ingredients.Commands.AddIngredientsToRecipe
{
    internal class AddIngredientsToRecipeHandler
        : IRequestHandler<AddIngredientsToRecipeCommand, IReadOnlyCollection<IngredientDto>>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddIngredientsToRecipeHandler(
            IRecipeCardRepository recipeRepeository, 
            IIngredientRepository ingredientRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeCardRepository = recipeRepeository;
            _ingredientRepository = ingredientRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyCollection<IngredientDto>> Handle(AddIngredientsToRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository.GetByIdWithIngredientsAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            var ingredientsResults = request.Items.Select(x => recipe!.AddIngredient(x.Name, x.Quantity)).ToArray();

            if (ingredientsResults.Any(x => x.HasError))
                Guard.ThrowBuisnessError(ingredientsResults.First(x => x.HasError).Error);

            var ingredients = ingredientsResults.Select(x => x.Entity).ToArray();

            _ingredientRepository.AddRange(ingredients);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return ingredients.Select(x => new IngredientDto(x.Id, x.Name, x.Quantity)).ToList();
            
        }
    }
}
