using MediatR;
using Recipes.Application.Core.Files;
using Recipes.Contracts.Recipes;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Application.UseCases.CookingStages.Commands.CreateCookingStage
{
    public class CreateCookingStageHandler : IRequestHandler<CreateCookingStageCommand, CookingStageDto>
    {
        private readonly ICookingStageRepository _cookingStageRepository;
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCookingStageHandler(
            ICookingStageRepository cookingStageRepository,
            IRecipeCardRepository recipeCardRepository,
            IUnitOfWork unitOfWork,
            IFileProvider fileProvider)
        {
            _cookingStageRepository = cookingStageRepository;
            _recipeCardRepository = recipeCardRepository;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
        }

        public async Task<CookingStageDto> Handle(CreateCookingStageCommand request, CancellationToken cancellationToken)
        {
            var recipeCard = await _recipeCardRepository.GetByIdWithDetailsAsync(request.RecipeCardId, cancellationToken);

            if (recipeCard is null)
                Guard.NotFound(RecipeCard.EntityName);

            if (recipeCard.Details is null)
                Guard.ThrowBuisnessError(CookingStageErrors.CantCreateStageWithoutDetails());

            var image = request.Stage.Image;

            var cookingStageImage = image == null
                ? default
                : await _fileProvider.CreateUploadFileAsync(
                    image.ContentType, image.FileName, image.Length, image.OpenReadStream(), cancellationToken);

            var cookingStage = recipeCard.Details!.CreateCookingState(
                image: cookingStageImage == null
                    ? default
                    : new CookingStageImage(
                        cookingStageImage.Content, cookingStageImage.ContentType, cookingStageImage.Size, cookingStageImage.FileName),
                description: request.Stage.Description);

            _cookingStageRepository.Add(cookingStage!);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CookingStageDto(
                Id: cookingStage.Id,
                Description: cookingStage.Description,
                Image: cookingStageImage?.Content);
        }
    }
}
