namespace Recipes.Contracts.Web
{
    public class PaginationResponse<T>
    {
        public bool Success => true;
        public IReadOnlyCollection<T> Data { get; }

        public int Count { get; }

        public int ItemsPerPage { get; }

        public PaginationResponse(IReadOnlyCollection<T> data, int count, int itemsPerPage)
        {
            Data = data;
            Count = count;
            ItemsPerPage = itemsPerPage;
        }

        public static PaginationResponse<T> FromList(List<T> data, int count, int itemsPerPage) =>
            new PaginationResponse<T>(data, count, itemsPerPage);

        public static PaginationResponse<T> FromEnumerable(IEnumerable<T> data, int count, int itemsPerPage) =>
            new PaginationResponse<T>(data.ToList(), count, itemsPerPage);
    }
}
