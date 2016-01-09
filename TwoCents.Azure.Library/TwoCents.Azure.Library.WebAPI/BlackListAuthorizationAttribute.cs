using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace TwoCents.Azure.Library.WebAPI
{
    //By default Web API uses white listing
    //White listing: All authorized users should be specified
    //Black listing: All unauthorized users should be specific
    //Example: [Authorize] -> All authenticated users are authorized
    //Example: [Authorize(Users = "srv_app_biztalk@bvgo.onmicrosoft.com")] -> Only user srv_app_biztalk is authorized
    //Example: [Authorize(Roles="Administrators")]
    //Example: [BlackListAuthorization(Users = "srv_app_biztalk@bvgo.onmicrosoft.com")] -> User srv_app_biztalk is unauthorized, everyone else is authorized
    public class BlackListAuthorizationAttribute : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            IPrincipal user = Thread.CurrentPrincipal;

            //There must be an authenticated user
            if (user == null) return false;

            //The users that you specify in your Authorize attribute are split
            var splitUsers = SplitString(Users);
            if (splitUsers.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
                return false;

            //The roles that you specify in your Authorize attribute are split
            var splitRoles = SplitString(Roles);
            if (splitRoles.Any(user.IsInRole))
                return false;

            return true;

        }

        private static string[] SplitString(string original)
        {

            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;

            return split.ToArray();

        }
    }
}
