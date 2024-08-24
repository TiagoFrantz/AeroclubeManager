using AeroclubeManager.Core.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace AeroclubeManager.Web.Attributes
{
    public class ConfirmedAccountAttribute : TypeFilterAttribute
    {
        public ConfirmedAccountAttribute() : base(typeof(ConfirmedAccountFilter))
        {
        }

        private class ConfirmedAccountFilter : IAsyncAuthorizationFilter
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public ConfirmedAccountFilter(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
            {
                var user = await _userManager.GetUserAsync(filterContext.HttpContext.User);

                if (user == null || !user.EmailConfirmed)
                {
                    filterContext.Result = new ForbidResult();
                }

            }
        }
    }
}
