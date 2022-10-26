namespace Recipes.Contracts.Web
{
    public class PaginationResponse<T>
    {
        public IReadOnlyCollection<T> Data { get; }

        public int Count { get; }

        public PaginationResponse(IReadOnlyCollection<T> data, int count)
        {
            Data = data;
            Count = count;
        }

        public static PaginationResponse<T> FromList(List<T> data, int count) =>
            new PaginationResponse<T>(data, count);

        public static PaginationResponse<T> FromEnumerable(IEnumerable<T> data, int count) =>
            new PaginationResponse<T>(data.ToList(), count);
    }
}
