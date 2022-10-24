using MediatR;
using Recipes.Contracts;
using Recipes.Domain.Core.Services;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.UseCases.Users.Commands.SignIn
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public SignInUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailOrLoginAsync(
                request.User.Login, request.User.Email, cancellationToken);

            if (user is null)
                Guard.NotFound(User.EntityName);

            var checkUserPasswordResult = user!.CheckUserPassword(request.User.Password, _passwordHasher);

            if (checkUserPasswordResult.HasError)
                Guard.ThrowBuisnessError(checkUserPasswordResult.Error);

            return new UserDto(user.Id, user.Email, user.Login);
        }
    }
}
