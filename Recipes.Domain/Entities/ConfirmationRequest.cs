using Recipes.Domain.Core;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Core.Events;
using Recipes.Domain.Enumerations;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Domain.Entities
{
    public class ConfirmationRequest : AggregateRoot<Guid>
    {
        public static string EntityName => nameof(ConfirmationRequest);
        public int RecipeId { get; private set; }

        public int? CreatedBy { get; private set; }

        public int? AcceptedBy { get; private set; }

        public int? RejectedBy { get; private set; }

        public DateTime CreateDate { get; private set; }

        public ConfirmationRequestStatus Status { get; private set; }

        public string? RejectedReason { get; private set; }

        public ConfirmationRequestCheckType CheckType { get; private set; }

        private ConfirmationRequest() { }

        protected ConfirmationRequest(Guid id, int recipeId, int? createdBy)
            : base(id)
        {
            RecipeId = recipeId;
            Status = ConfirmationRequestStatus.Pending;
            CheckType = ConfirmationRequestCheckType.Unknown;
            CreatedBy = createdBy;
            RaiseEvent(new ConfirmationRequestCreated(id));
        }

        public static Result<ConfirmationRequest> Create(Guid id, RecipeCard recipe, int? createdBy)
        {
            return new ConfirmationRequest(id, recipe.Id, createdBy);
        }

        public Result<ConfirmationRequest> Accept(int? acceptedBy = null)
        {
            if (Status != ConfirmationRequestStatus.Pending)
            {
                return ConfirmationRequestErrors.InvalidRequestStatus();
            }

            var canAcceptResult = CheckType.CanAccept(acceptedBy);
            if (canAcceptResult.HasError)
                return canAcceptResult.Error;

            AcceptedBy = acceptedBy;
            Status = ConfirmationRequestStatus.Accepted;

            return this;
        }

        public Result<ConfirmationRequest> Reject(int rejectedBy, string rejectedReason)
        {
            if (Status != ConfirmationRequestStatus.Pending)
            {
                return ConfirmationRequestErrors.InvalidRequestStatus();
            }

            var canRejectResult = CheckType.CanReject();
            if (canRejectResult.HasError)
                return canRejectResult.Error;

            if (string.IsNullOrEmpty(rejectedReason))
                return ConfirmationRequestErrors.InvalidRejectedReason();

            RejectedBy = rejectedBy;
            Status = ConfirmationRequestStatus.Rejected;
            RejectedReason = rejectedReason;

            return this;
        }

        public Result<ConfirmationRequest> ProcessManually()
        {
            if (CheckType != ConfirmationRequestCheckType.Unknown)
            {
                return ConfirmationRequestErrors.AlreadyProcessed();
            }

            CheckType = ConfirmationRequestCheckType.Manual;

            return this;
        }

        public Result<ConfirmationRequest> ProcessAutomatically()
        {
            if (CheckType != ConfirmationRequestCheckType.Unknown)
            {
                return ConfirmationRequestErrors.AlreadyProcessed();
            }

            CheckType = ConfirmationRequestCheckType.Automatic;

            return this;
        }
    }
}
