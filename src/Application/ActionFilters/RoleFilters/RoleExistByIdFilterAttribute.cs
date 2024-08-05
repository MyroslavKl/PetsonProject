using System.Net;
using Application.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.ActionFilters.RoleFilters;

public class RoleExistByIdFilterAttribute(IRoleRepository roleRepository):ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        int id = (int)context.ActionArguments["roleId"];
        if (await roleRepository.GetOneAsync(role => role.Id == id) is null)
        {
            context.ModelState.AddModelError("Role", "Role does not exist.");
            ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
            {
                Status = HttpStatusCode.NotFound as int?
            };
            context.Result = new NotFoundObjectResult(details);
            return;
        }
        await next();
    }
    
}