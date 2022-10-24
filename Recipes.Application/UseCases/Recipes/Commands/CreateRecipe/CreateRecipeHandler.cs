﻿using MediatR;
using Recipes.Contracts;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Application.UseCases.Recipes.Commands.CreateRecipe
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecipeHandler(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RecipeDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var hashtags = request.Recipe.HashTags?.Select(x => new Hashtag(x.Title)).ToList();

            var ingridients = request.Recipe.Ingredients?.Select(x => new Ingredient(x.Name, x.Quantity)).ToList();

            var recipeResult = Recipe.Create(
                request.Recipe.Title,
                request.Recipe.CookingProcess,
                hashtags,
                ingridients!);

            if (recipeResult.HasError)
                Guard.ThrowBuisnessError(recipeResult.Error);

            _recipeRepository.Add(recipeResult.Entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return request.Recipe with { Id = recipeResult.Entity.Id };
        }
    }
}
