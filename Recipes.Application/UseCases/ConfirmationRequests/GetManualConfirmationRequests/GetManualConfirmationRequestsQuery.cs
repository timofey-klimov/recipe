using MediatR;
using Recipes.Contracts.ConfirmationRequests;

namespace Recipes.Application.UseCases.ConfirmationRequests.GetManualConfirmationRequests
{
    public record GetManualConfirmationRequestsQuery() : IRequest<IEnumerable<ConfirmationRequestDto>>;
    
}
