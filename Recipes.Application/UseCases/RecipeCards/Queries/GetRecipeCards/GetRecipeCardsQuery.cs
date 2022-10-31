﻿using MediatR;
using Recipes.Contracts.Recipes;
using Recipes.Contracts.Web;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCards
{
    public record GetRecipeCardsQuery(int Page) : IRequest<PaginationResponse<RecipeCardDto>>;
}
