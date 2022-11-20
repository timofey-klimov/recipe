namespace Recipes.Contracts.Web
{
    public class PaginationResponse<T>
    {
        public bool Success => true;
        public IReadOnlyCollection<T> Data { get; }

        public int TotalPages { get; }
        public PaginationResponse(IReadOnlyCollection<T> data, int totalPages)
        {
            Data = data;
            TotalPages = totalPages;
        }

        public static PaginationResponse<T> FromList(List<T> data, int totalPages) =>
            new PaginationResponse<T>(data, totalPages);

        public static PaginationResponse<T> FromEnumerable(IEnumerable<T> data, int totalPages) =>
            new PaginationResponse<T>(data.ToList(), totalPages);
    }
}
