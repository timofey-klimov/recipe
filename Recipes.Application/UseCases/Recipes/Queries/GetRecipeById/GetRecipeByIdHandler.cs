using AutoMapper;
using MediatR;
using Recipes.Contracts;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.Recipes.Queries.GetRecipeById
{
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public GetRecipeByIdHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<RecipeDto> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetByIdWithIngredientsAndHashtags(request.Id, cancellationToken);

            if (recipe is null)
                Guard.NotFound(Recipe.EntityName);

            return _mapper.Map<RecipeDto>(recipe);
        }
    }
}
