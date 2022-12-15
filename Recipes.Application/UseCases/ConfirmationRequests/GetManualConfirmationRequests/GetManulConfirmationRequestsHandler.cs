using MediatR;
using Recipes.Application.Core.Identity;
using Recipes.Application.Core.Identity.Enums;
using Recipes.Contracts.ConfirmationRequests;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.ConfirmationRequests.GetManualConfirmationRequests
{
    public class GetManulConfirmationRequestsHandler
        : IRequestHandler<GetManualConfirmationRequestsQuery, IEnumerable<ConfirmationRequestDto>>
    {
        private readonly IPermissionProvider _permissionProvider;
        private readonly IConfirmationRequestRepository _confirmationRequestRepository;
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetManulConfirmationRequestsHandler(
            IPermissionProvider permissionProvider,
            IConfirmationRequestRepository confirmationRequestRepository,
            IRecipeCardRepository recipeCardRepository)
        {
            _permissionProvider = permissionProvider;
            _confirmationRequestRepository = confirmationRequestRepository;
            _recipeCardRepository = recipeCardRepository;
        }

        public async Task<IEnumerable<ConfirmationRequestDto>> Handle(GetManualConfirmationRequestsQuery request, CancellationToken cancellationToken)
        {
            var hasPermission = 
                await _permissionProvider.HasPermissionsAsync(
                    new List<Permissions> { Permissions.ApproveCreateRecipesRequest });

            if (!hasPermission)
                Guard.Forbidden();

            var confirmationRequests = await _confirmationRequestRepository.GetForManulApproveAsync(cancellationToken);
            var result = new List<ConfirmationRequestDto>(confirmationRequests.Count);

            foreach (var confirmationRequest in confirmationRequests)
            {
                var recipeCard = await _recipeCardRepository.GetByIdAsync(confirmationRequest.RecipeId, cancellationToken);

                if (recipeCard is null)
                    Guard.NotFound(RecipeCard.EntityName);

                result.Add(new ConfirmationRequestDto
                {
                    Status = confirmationRequest.Status.Name,
                    CheckType = confirmationRequest.CheckType.Name,
                    RecipeImage = recipeCard.ImageSource,
                    RecipeName = recipeCard.Title,
                    RejectedReason = confirmationRequest.RejectedReason
                });
            }

            return result;
        }
    }
}
