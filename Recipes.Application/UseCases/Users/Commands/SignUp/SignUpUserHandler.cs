using MediatR;
using Recipes.Contracts;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Core.Services;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.Users.Commands.CreateUser
{
    public class SignUpUserHandler : IRequestHandler<SignUpUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public SignUpUserHandler(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher, 
            ISaltGenerator saltGenerator, 
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordHasher = passwordHasher;
            _saltGenerator = saltGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            var (login, email, password) = request.User;

            var createUserResult = await User.CreateAsync(login, email, password, _userRepository, _passwordHasher, _saltGenerator);

            if (createUserResult.HasError)
                Guard.ThrowBuisnessError(createUserResult.Error);

            var user = createUserResult.Entity;

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UserDto(user.Id, user.Email, user.Login);
        }
    }
}
