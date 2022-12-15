using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.ConfirmationRequests.GetManualConfirmationRequests;
using Recipes.Application.UseCases.ConfirmationRequests.GetUserConfirmationRequests;
using Recipes.Contracts.ConfirmationRequests;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Authorize]
    [Route("api/confirmationRequests")]
    public class ConfirmationRequestsController : ApplicationController
    {
        public ConfirmationRequestsController(IMediator mediator) 
            : base(mediator)
        {
        }

        /// <summary>
        /// Получение заявок пользователя на подтверждение рецепта
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("userRequests")]
        public async Task<Response<IEnumerable<ConfirmationRequestDto>>> GetUserConfirmationRequests(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetUserConfirmationRequestsQuery()));
        }

        /// <summary>
        /// Получения заявок на ручное подтверждение
        /// </summary>
        /// <returns></returns>
        [HttpGet("manualApproveRequests")]
        public async Task<Response<IEnumerable<ConfirmationRequestDto>>> GetManulApproveRequests()
        {
            return Ok(await Mediator.Send(new GetManualConfirmationRequestsQuery()));
        }
    }
}
