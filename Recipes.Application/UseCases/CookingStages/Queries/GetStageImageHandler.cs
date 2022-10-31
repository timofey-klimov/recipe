using MediatR;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.UseCases.CookingStages.Queries
{
    public class GetStageImageHandler : IRequestHandler<GetStageImageQuery, (byte[] Content, string ContentType)>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetStageImageHandler(IRecipeCardRepository recipeCardRepository)
        {
            _recipeCardRepository = recipeCardRepository;
        }

        public async Task<(byte[] Content, string ContentType)> Handle(GetStageImageQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository
                .GetByIdWithStagesAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            var stage = recipe!.Stages.FirstOrDefault(x => x.Id == request.StageId);

            if (stage is null)
                Guard.NotFound(CookingStage.EntityName);

            if (stage.Image is null)
                Guard.NotFound(CookingStageImage.ValueObjectName);

            return (stage.Image!.Content, stage.Image.ContentType);
        }
    }
}
