using System.Net;
using Application.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.ActionFilters.UserFilters;

public class UserExistByIdFilterAttribute(IUserRepository userRepository):ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        int id = (int)context.ActionArguments["userId"];
        if (await userRepository.GetOneAsync(user => user.Id == id) is null)
        {
            context.ModelState.AddModelError("User", "User does not exist.");
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