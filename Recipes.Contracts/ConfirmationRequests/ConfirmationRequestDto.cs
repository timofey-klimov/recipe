namespace Recipes.Contracts.ConfirmationRequests
{
    public class ConfirmationRequestDto
    {
        public string Status { get; set; }

        public string CheckType { get; set; }

        public string RecipeName { get; set; }

        public string RecipeImage { get; set; }

        public string? RejectedReason { get; set; }
    }
}
