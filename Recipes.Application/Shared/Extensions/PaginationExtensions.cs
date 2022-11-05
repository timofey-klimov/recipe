using Recipes.Contracts.Web;

namespace Recipes.Application.Shared.Extensions
{
    public static class PaginationExtensions
    {
        public static PaginationResponse<T> ToPagination<T>(this IEnumerable<T> source, int count, int itemsPerPage) => 
            PaginationResponse<T>.FromList(source.ToList(), count, itemsPerPage);
    }
}
