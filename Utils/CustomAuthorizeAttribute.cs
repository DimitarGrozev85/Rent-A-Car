namespace Rent_a_car.Utils
{
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string userRoles) : base(typeof(CustomAuthorization))
        {
            Arguments = new object[] { userRoles };
        }

        private class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter
        {
            private readonly Rent_a_carContext _dbContext;
            private readonly string _userRoles;

            public CustomAuthorization(Rent_a_carContext dbContext, string userRoles)
            {
                _dbContext = dbContext;
                _userRoles = userRoles;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (string.IsNullOrEmpty(_userRoles))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var userEmail = context.HttpContext.Session.GetString("Email");
                if (string.IsNullOrEmpty(userEmail))
                    context.Result = new UnauthorizedResult();

                var assignedUserRole = _dbContext.Users.FirstOrDefault(user => user.Email == userEmail)?.UserRole.ToString();

                if (string.IsNullOrEmpty(assignedUserRole))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var requiredUserRoles = _userRoles.Split(",");
                if (requiredUserRoles.Contains(assignedUserRole))
                    return;

                context.Result = new UnauthorizedResult();
            }
        }
    }
}
