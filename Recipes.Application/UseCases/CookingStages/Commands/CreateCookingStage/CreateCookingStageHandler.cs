using MediatR;
using Recipes.Application.Core.Files;
using Recipes.Contracts.CookingStages;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.UseCases.CookingStages.CreateCookingStage
{
    public class CreateCookingStageHandler : IRequestHandler<CreateCookingStageCommand, CookingStageDto>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly ICookingStageRepository _cookingStageRepository;
        private readonly IFileProviderFactory _fileProviderFactory;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCookingStageHandler(
            IRecipeCardRepository recipeCardRepository,
            ICookingStageRepository cookingStageRepository,
            IFileProviderFactory fileProviderFactory,
            IUnitOfWork unitOfWork)
        {
            _recipeCardRepository = recipeCardRepository;
            _cookingStageRepository = cookingStageRepository;
            _fileProviderFactory = fileProviderFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task<CookingStageDto> Handle(CreateCookingStageCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository
                .GetByIdWithStagesAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            var file = request.Image;

            var fileProvider = _fileProviderFactory.GetPhysicalFileProvider();

            var stageResult = recipe!.CreateRecipeStage(
                description: request.Description,
                imagePath: file == null
                    ? default
                    : await fileProvider.SaveFileAsync(file, cancellationToken));

            if (stageResult.HasError)
                Guard.ThrowBuisnessError(stageResult.Error);

            _cookingStageRepository.Add(stageResult.Entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CookingStageDto(stageResult.Entity.Id,
                stageResult.Entity.Description,
                stageResult.Entity.ImageSource);
        }
    }
}
