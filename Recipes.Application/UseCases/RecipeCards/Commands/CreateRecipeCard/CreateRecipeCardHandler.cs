using MediatR;
using Recipes.Application.Core.Auth;
using Recipes.Application.Core.Files;
using Recipes.Application.Core.Identity;
using Recipes.Contracts.Recipes;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Enumerations;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard
{
    public class CreateRecipeCardHandler : IRequestHandler<CreateRecipeCardCommand, RecipeCardDto>
    {
        private readonly IRecipeCardRepository _repository;
        private readonly IConfirmationRequestRepository _confirmationRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserProvider _userProvider;
        private readonly IFileProviderFactory _fileProviderFactory;
        private readonly IGuidProvider _guidProvider;
        public CreateRecipeCardHandler(
            IRecipeCardRepository repository, 
            IUnitOfWork unitOfWork,
            ICurrentUserProvider userProvider,
            IFileProviderFactory fileProviderFactory,
            IConfirmationRequestRepository confirmationRequestRepository,
            IGuidProvider guidProvider)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _fileProviderFactory = fileProviderFactory;
            _confirmationRequestRepository = confirmationRequestRepository;
            _guidProvider = guidProvider;
        }

        public async Task<RecipeCardDto> Handle(CreateRecipeCardCommand request, CancellationToken cancellationToken)
        {
            var createdBy = _userProvider.UserId;

            var fileProvider = _fileProviderFactory.GetPhysicalFileProvider();
            var imageSource = await fileProvider.SaveFileAsync(request.File, cancellationToken);
            var mealType = MealEnumeration.FromValue(request.MealType);
            if (mealType == null)
                Guard.ThrowBuisnessError(RecipeCardErrors.IncorrectMealType());

            var recipeResult = RecipeCard
                .Create(request.Title, request.Remark, mealType, createdBy, imageSource);

            if (recipeResult.HasError)
                Guard.ThrowBuisnessError(recipeResult.Error);

            var recipeCard = recipeResult.Entity;

            using var tran = await _unitOfWork.BeginTransactionAsync();
            try
            {
                _repository.Add(recipeCard);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var confirmationRequestResult = ConfirmationRequest.Create(
                    _guidProvider.Create(),
                    recipeCard,
                    createdBy);

                if (confirmationRequestResult.HasError)
                    Guard.ThrowBuisnessError(confirmationRequestResult.Error);

                _confirmationRequestRepository.Add(confirmationRequestResult.Entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await tran.CommitAsync();
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                throw ex;
            }

            return new RecipeCardDto(
                recipeCard.Id,
                recipeCard.Title, 
                mealType.Name,
                recipeCard.CreateDate.ToShortDateString(),
                recipeCard.ImageSource);
        }
    }
}
