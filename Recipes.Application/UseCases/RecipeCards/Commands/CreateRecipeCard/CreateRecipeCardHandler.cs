using MediatR;
using Recipes.Application.Core.Auth;
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
        private readonly ICurrentUserProvider _userProvider;
        private readonly IFileProviderFactory _fileProviderFactory;
        public CreateRecipeCardHandler(
            IRecipeCardRepository repository, 
            IUnitOfWork unitOfWork,
            ICurrentUserProvider userProvider,
            IFileProviderFactory fileProviderFactory)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _fileProviderFactory = fileProviderFactory;
        }

        public async Task<RecipeCardDto> Handle(CreateRecipeCardCommand request, CancellationToken cancellationToken)
        {
            var createdBy = _userProvider.UserId;

            var fileProvider = _fileProviderFactory.GetPhysicalFileProvider();
            var imageSource = await fileProvider.SaveFileAsync(request.File, cancellationToken);

            var recipeResult = RecipeCard
                .Create(request.Title, request.Remark, request.MealType, createdBy, imageSource);

            if (recipeResult.HasError)
                Guard.ThrowBuisnessError(recipeResult.Error);

            var entity = recipeResult.Entity;

            _repository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RecipeCardDto(
                entity.Id, 
                entity.Title, 
                (byte)entity.MealType, 
                entity.CreateDate.ToShortDateString(),
                entity.ImageSource);
        }
    }
}
