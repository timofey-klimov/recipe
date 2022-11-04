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
        private Lazy<int?> _userId;
        public CurrentUserProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _userId = new Lazy<int?>(() => GetUserId());
        }

        public int? UserId => _userId.Value;

        private int? GetUserId()
        {
            var contextUser = _contextAccessor.HttpContext.User;
            var idClaim = contextUser?.Claims?.FirstOrDefault(x => x.Value == "id");

            return idClaim == null
                ? default(int?)
                : int.Parse(idClaim.Value);
        }
    }
}
