using System.Net;
using Application.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.ActionFilters.PetFilters;

public class PetExistByTypeFilterAttribute(IPetRepository petRepository):ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string type = (string)context.ActionArguments["type"];
        if (await petRepository.GetOneAsync(obj => obj.TypeOfPet == type) is null)
        {
            context.ModelState.AddModelError("Pet", "Pet does not exist.");
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