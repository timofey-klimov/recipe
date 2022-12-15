using MediatR;
using Recipes.Application.Core.Identity;
using Recipes.Application.Core.Identity.Enums;
using Recipes.Domain.Core.Events;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.ConfirmationRequests.Events
{
    public class ApproveConfirmationRequestHandler : INotificationHandler<ConfirmationRequestCreated>
    {
        private readonly IPermissionProvider _permissionProvider;
        private readonly IConfirmationRequestRepository _confirmationRequestRepository;
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveConfirmationRequestHandler(
            IPermissionProvider permissionProvider,
            IConfirmationRequestRepository confirmationRequestRepository,
            IRecipeCardRepository recipeCardRepository,
            IUnitOfWork unitOfWork)
        {
            _permissionProvider = permissionProvider;
            _confirmationRequestRepository = confirmationRequestRepository;
            _recipeCardRepository = recipeCardRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ConfirmationRequestCreated notification, CancellationToken cancellationToken)
        {
            var request = await _confirmationRequestRepository.GetByIdAsync(notification.ConfirmationRequestId);

            var hasPermissions =
                await _permissionProvider.HasPermissionsAsync(
                    request.CreatedBy,
                        new List<Permissions> { Permissions.ApproveCreateRecipesRequest });

            if (hasPermissions)
            {
                var processedRequestResult = request.ProcessAutomatically();

                if (processedRequestResult.HasError)
                    Guard.ThrowBuisnessError(processedRequestResult.Error);

                var acceptedRequestResult = processedRequestResult.Entity.Accept();

                var recipeCard = await _recipeCardRepository.GetByIdAsync(request.RecipeId, cancellationToken);

                if (recipeCard == null)
                    Guard.NotFound(RecipeCard.EntityName);

                var acceptedRecipeCardResult = recipeCard.Accept(acceptedRequestResult.Entity);

                if (acceptedRecipeCardResult.HasError)
                    Guard.ThrowBuisnessError(acceptedRecipeCardResult.Error);

                using var tran = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    _confirmationRequestRepository.Update(acceptedRequestResult.Entity);
                    _recipeCardRepository.Update(acceptedRecipeCardResult.Entity);
                    await _unitOfWork.SaveChangesAsync();
                    await tran.CommitAsync();
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
            else
            {
                var processedManuallyRequestResult = request.ProcessManually();
                if (processedManuallyRequestResult.HasError)
                    Guard.ThrowBuisnessError(processedManuallyRequestResult.Error);

                _confirmationRequestRepository.Update(processedManuallyRequestResult.Entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
