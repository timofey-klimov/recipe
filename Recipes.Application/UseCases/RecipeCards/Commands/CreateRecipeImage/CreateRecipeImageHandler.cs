using MediatR;
using Recipes.Application.Core.Files;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeImage
{
    public class CreateRecipeImageHandler : IRequestHandler<CreateRecipeImageCommand, Unit>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRecipeImageHandler(
            IRecipeCardRepository recipeCardRepository, 
            IFileProvider fileProvider,
            IUnitOfWork unitOfWork)
        {
            _recipeCardRepository = recipeCardRepository;
            _fileProvider = fileProvider;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateRecipeImageCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository
                .GetByIdWithImageAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            var uploadedFile = await _fileProvider.CreateUploadFileAsync(
                request.File.ContentType,
                request.File.FileName,
                request.File.Length,
                request.File.OpenReadStream(),
                cancellationToken);

            _ = recipe!
                .CreateImage(uploadedFile.Content, uploadedFile.ContentType, uploadedFile.Size, uploadedFile.FileName);

            _recipeCardRepository.Update(recipe);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
