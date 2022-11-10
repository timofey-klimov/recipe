namespace Recipes.Contracts.Web
{
    public class Response<T>
    {
        public bool Success { get; }
        public T Data { get; }

        public Response(T data, bool success)
        {
            Data = data;
            Success = success;
        }

        public static Response<T> Create(T data, bool success) => new Response<T>(data, success);
    }

}
