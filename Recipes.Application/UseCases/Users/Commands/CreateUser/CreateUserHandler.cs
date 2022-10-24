using MediatR;
using Recipes.Contracts;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Core.Services;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserHandler(
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

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            var (_, email, login, password) = request.User;

            var createUserResult = await User.CreateAsync(login, email, password, _userRepository, _passwordHasher, _saltGenerator);

            if (!createUserResult.IsSuccess)
                Guard.ThrowBuisnessError(createUserResult.Error);

            _userRepository.Add(createUserResult.Entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return request.User with { Id = createUserResult.Entity.Id };
        }
    }
}
