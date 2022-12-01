using MediatR;
using Recipes.Domain.Repositories;

namespace Recipes.Application.UseCases.Users.Queries.CheckUserExists
{
    public class CheckUserExistsHandler : IRequestHandler<CheckUserExistsQuery, bool>
    {
        private readonly IUserRepository _userRepository;
        public CheckUserExistsHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        {
            return  await _userRepository.GetUserByEmailOrLoginAsync(request.UserInfo, request.UserInfo, cancellationToken) 
                != null;
        }
    }
}
