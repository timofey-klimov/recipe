using Recipes.Domain.Core;

namespace Recipes.Domain.Shared
{
    public class Result
    {
        public Error Error { get; }
        private bool _isSuccess { get; }

        public bool HasError => !_isSuccess;
        public Result(Error error, bool isSuccess)
        {
            Error = error;
            _isSuccess = isSuccess;
        }

        public static Result FromError(Error error) => new Result(error, false);

        public static Result Success() => new Result(default, true);

        public static implicit operator Result(Error error) => FromError(error);
    }

    public class Result<T>
    {
        private T _entity;
        private bool _isSuccess;
        private Result(T entity, Error error, bool isSuccess)
        {
            _entity = entity;
            Error = error;
            _isSuccess = isSuccess;
        }

        public T Entity
        {
            get
            {
                if (HasError)
                    throw new InvalidOperationException();

                return _entity;
            }
            set
            {
                _entity = value;
            }
        }

        public bool HasError => !_isSuccess;

        public Error Error { get; }




        public static Result<T> FromValue(T entity) => new Result<T>(entity, default, true);
        public static Result<T> FromError(Error error) => new Result<T>(default, error, false);

        public static implicit operator Result<T>(T entity) => FromValue(entity);
        public static implicit operator Result<T>(Error error) => FromError(error);
    }
}
