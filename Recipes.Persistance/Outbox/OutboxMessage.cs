namespace Recipes.Persistance.Outbox
{
    public sealed class OutboxMessage
    {
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime OccuredOn { get; set; }

        public DateTime? ProccededOn { get; set; }

        public string? Error { get; set; }
    }
}
