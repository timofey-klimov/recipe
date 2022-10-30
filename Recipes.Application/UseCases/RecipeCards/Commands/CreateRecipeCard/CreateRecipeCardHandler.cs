using MediatR;
using Recipes.Application.Core.Files;
using Recipes.Contracts.Recipes;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard
{
    public class CreateRecipeCardHandler : IRequestHandler<CreateRecipeCardCommand, RecipeCardDto>
    {
        private readonly IRecipeCardRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecipeCardHandler(
            IRecipeCardRepository repository, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RecipeCardDto> Handle(CreateRecipeCardCommand request, CancellationToken cancellationToken)
        {
            var recipeResult = RecipeCard
                .Create(request.Title, request.Remark, request.MealType, request.Hashtags?.ToList());

            if (recipeResult.HasError)
                Guard.ThrowBuisnessError(recipeResult.Error);

            var entity = recipeResult.Entity;

            _repository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RecipeCardDto(
                entity.Id, 
                entity.Title, 
                entity.Remark, 
                (byte)entity.MealType, 
                entity.CreateDate.ToShortDateString(),
                entity.Hashtags?.Select(x => x.Title).ToList());
        }
    }
}
