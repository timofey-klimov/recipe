namespace Recipes.Contracts.Services.Files
{
    public record UploadFile(string ContentType, string? FileName, long Size, byte[] Content);
    
}
