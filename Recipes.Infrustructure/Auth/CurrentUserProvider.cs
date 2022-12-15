using Microsoft.AspNetCore.Http;
using Recipes.Application.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Infrustructure.Auth
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            UserId = GetUserId();
        }

        public int? UserId { get; }

        private int? GetUserId()
        {
            var contextUser = _contextAccessor?.HttpContext?.User;
            var idClaim = contextUser?.Claims?.FirstOrDefault(x => x.Type == "id");

            return idClaim == null
                ? default(int?)
                : int.Parse(idClaim.Value);
        }
    }
}
