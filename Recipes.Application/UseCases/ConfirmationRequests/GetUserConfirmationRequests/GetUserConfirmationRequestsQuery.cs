using MediatR;
using Recipes.Contracts.ConfirmationRequests;

namespace Recipes.Application.UseCases.ConfirmationRequests.GetUserConfirmationRequests
{
    public record GetUserConfirmationRequestsQuery : IRequest<IEnumerable<ConfirmationRequestDto>>;
    
}
