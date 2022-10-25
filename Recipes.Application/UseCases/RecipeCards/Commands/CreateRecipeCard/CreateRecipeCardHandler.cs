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
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecipeCardHandler(
            IRecipeCardRepository repository, 
            IFileProvider fileProvider, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _fileProvider = fileProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<RecipeCardDto> Handle(CreateRecipeCardCommand request, CancellationToken cancellationToken)
        {
            var uploadFile = await _fileProvider.CreateUploadFileAsync(
                request.Image.ContentType,
                request.Image.FileName,
                request.Image.Length,
                request.Image.OpenReadStream(),
                cancellationToken);

            var recipeCardMainImage = new RecipeMainImage(
                uploadFile.Content, uploadFile.ContentType, uploadFile.Size, uploadFile.FileName);

            var recipeCardResult = RecipeCard.Create(request.Title, recipeCardMainImage);

            if (recipeCardResult.HasError)
                Guard.ThrowBuisnessError(recipeCardResult.Error);

            var recipeCard = recipeCardResult.Entity;

            _repository.Add(recipeCard);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RecipeCardDto(recipeCard.Id, recipeCard.Title, recipeCard.Image.Content);
        }
    }
}
