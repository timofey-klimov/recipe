using MediatR;
using Recipes.Application.Core.Auth;
using Recipes.Contracts.ConfirmationRequests;
using Recipes.Domain.Repositories;

namespace Recipes.Application.UseCases.ConfirmationRequests.GetUserConfirmationRequests
{
    public class GetUserConfirmationRequestsHandler
        : IRequestHandler<GetUserConfirmationRequestsQuery, IEnumerable<ConfirmationRequestDto>>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IConfirmationRequestRepository _confirmationRequestRepository;
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetUserConfirmationRequestsHandler(
            ICurrentUserProvider currentUserProvider,
            IConfirmationRequestRepository confirmationRequestRepository,
            IRecipeCardRepository recipeCardRepository)
        {
            _currentUserProvider = currentUserProvider;
            _confirmationRequestRepository = confirmationRequestRepository;
            _recipeCardRepository = recipeCardRepository;
        }

        public async Task<IEnumerable<ConfirmationRequestDto>> Handle(GetUserConfirmationRequestsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserProvider.UserId;

            var confirmationRequests = await _confirmationRequestRepository.GetByUserIdAsync(userId!.Value, cancellationToken);

            var result = new List<ConfirmationRequestDto>();

            foreach (var confirmationRequest in confirmationRequests)
            {
                var recipeCard = await _recipeCardRepository.GetByIdAsync(confirmationRequest.RecipeId, cancellationToken);

                result.Add(new ConfirmationRequestDto
                {
                    CheckType = confirmationRequest.CheckType.Name,
                    Status = confirmationRequest.Status.Name,
                    RecipeImage = recipeCard.ImageSource,
                    RecipeName = recipeCard.Title,
                    RejectedReason = confirmationRequest.RejectedReason
                });
            }

            return result;
        }
    }
}
