using Microsoft.AspNetCore.Http;

namespace Recipes.Contracts.Web
{
    public record FileRequest(IFormFile File);
}
