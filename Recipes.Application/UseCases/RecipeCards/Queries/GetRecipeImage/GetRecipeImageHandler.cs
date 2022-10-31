using MediatR;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeImage
{
    public class GetRecipeImageHandler : IRequestHandler<GetRecipeImageQuery, (byte[] Content, string ContentType)>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetRecipeImageHandler(IRecipeCardRepository recipeCardRepository)
        {
            _recipeCardRepository = recipeCardRepository;
        }
        public async Task<(byte[] Content, string ContentType)> Handle(GetRecipeImageQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository
                .GetByIdWithImageAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            if (recipe.Image is null)
                Guard.ThrowBuisnessError(RecipeCardErrors.ImageIsNotCreated());

            return (recipe.Image.Content, recipe.Image.ContentType);
        }
    }
}
