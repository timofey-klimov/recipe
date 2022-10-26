using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class CookingStageErrors
    {
        public static Error CantCreateStageWithoutDetails() =>
            new Error("CookingState.DetailsIsEmpty", "Details should be created before adding cooking stage");
    }
}
