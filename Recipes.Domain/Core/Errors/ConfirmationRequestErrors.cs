using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class ConfirmationRequestErrors
    {
        public static Error InvalidRequestStatus() =>
            new Error("ConfirmationRequest.InvalidRequestStatus", "Не верный статус заявки");

        public static Error InvalidActionInCheckType() =>
            new Error("ConfirmationRequest.InvalidActionInCheckType", "Не верный тип подтверждения");

        public static Error InvalidRejectedReason() =>
            new Error("ConfirmationRequest.InvalidRejectedReason", "Причина отказа не заполнена");

        public static Error AlreadyProcessed() =>
            new Error("ConfirmationRequest.AlreadyProcessed", "Заявка уже обработана");
    }
}
