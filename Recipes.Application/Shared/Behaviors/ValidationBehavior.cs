﻿using FluentValidation;
using MediatR;
using Recipes.Application.Shared.Exceptions;

namespace Recipes.Application.Shared.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null)
                return await next();

            var validationContext = new ValidationContext<TRequest>(request);

            var validationResult = await _validator.ValidateAsync(validationContext, cancellationToken);

            if (validationResult.IsValid)
                return await next();

            var errorsDictionary = validationResult.Errors
                .GroupBy(x => x.ErrorCode, (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Errors = errorMessages.Select(x => x.ErrorMessage).Distinct().ToArray()
                })
                .ToDictionary(x => x.Key, x => x.Errors);

            throw new AppValidationException(errorsDictionary);
        }
    }
}
