namespace Recipes.Contracts.Web
{
    public class Response<T>
    {
        public T Data { get; }

        public Response(T data)
        {
            Data = data;
        }

        public static Response<T> Create(T data) => new Response<T>(data);
    }
}
