using Recipes.Domain.Core;

namespace Recipes.Domain.Shared
{
    public class Result<T>
        where T : Entity
    {
        private T _entity;
        private Result(T entity, Error error, bool isSuccess)
        {
            _entity = entity;
            Error = error;
            IsSuccess = isSuccess;
        }

        public T Entity
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _entity;
            }
            set
            {
                _entity = value;
            }
        }

        public Error Error { get; }

        public bool IsSuccess { get; }

        public IReadOnlyCollection<Error> Errors { get; }

        public static Result<T> FromValue(T entity) => new Result<T>(entity, default, true);
        public static Result<T> FromError(Error error) => new Result<T>(default, error, false);

        public static implicit operator Result<T>(T entity) => FromValue(entity);
        public static implicit operator Result<T>(Error error) => FromError(error);
    }
}
