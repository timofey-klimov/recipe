using MediatR;
using Recipes.Application.Core.Auth;
using Recipes.Domain.Core.Services;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Application.UseCases.Users.Commands.SignIn
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public SignInUserHandler(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher,
            IJwtTokenProvider jwtTokenProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<string> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailOrLoginAsync(
                request.User.Login, request.User.Email, cancellationToken);

            if (user is null)
                Guard.NotFound(User.EntityName);

            var passwordResult = Password.Create(request.User.Password);
            if (passwordResult.HasError)
                Guard.ThrowBuisnessError(passwordResult.Error);

            var checkUserPasswordResult = user!.CheckUserPassword(passwordResult.Entity, _passwordHasher);

            if (checkUserPasswordResult.HasError)
                Guard.ThrowBuisnessError(checkUserPasswordResult.Error);

            return _jwtTokenProvider.CreateToken(user);
        }
    }
}
