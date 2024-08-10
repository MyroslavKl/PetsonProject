using System.Net;
using Application.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.ActionFilters.ReserveFilters;

public class ReserveExistByDateFilterAttribute(IReserveRepository reserveRepository):ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        DateTime date = (DateTime)context.ActionArguments["date"];
        if (await reserveRepository.GetOneAsync(obj => obj.ReserveDate == date) is null)
        {
            context.ModelState.AddModelError("Reserve", "Reserve does not exist.");
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