using ElectronicVoting.Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ElectronicVoting.Validator.Infrastructure.Attribute
{
    public class VerifyTokenAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string linkAuth = "";
            var token = context.HttpContext.Request.Headers["Authorization"];
            if(!string.IsNullOrEmpty(token))
            {
                var response = HttpHelper.GetAsync(linkAuth, "", CancellationToken.None).Result;
                
            }
        }
    }
}
