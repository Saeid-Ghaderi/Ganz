using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Ganz.Application.Interfaces;

namespace Ganz.API.CustomAttributes
{
    public class AccessControlAttribute : ActionFilterAttribute
    {
        public string Permission { get; set; }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(q => q.Type == "userId").Value);
            // var roles = GetRoles(userId);
            // var permissions = GetPermissions(roles);

            //1-inject IPermissionService
            //2-Call CheckPermission from IPermissionService

            //get di instance from HttpContext.RequstServices
            var permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();

            if (!await permissionService.CheckPermission(userId, Permission))
            {
                context.Result = new BadRequestObjectResult("not access in action");
            }
            else
            {
                //base.OnActionExecutionAsync(context, next);
                await next();
            }
        }
    }
}
