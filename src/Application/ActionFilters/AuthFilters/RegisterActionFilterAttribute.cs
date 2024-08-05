using System.Net;
using Application.DTOs.UserDTOs;
using Application.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.ActionFilters.AuthFilters;

public class RegisterActionFilterAttribute(IUserRepository userRepository): ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        CreateUserDto createUserDto = (CreateUserDto)context.ActionArguments["createUserDto"];

        if (await userRepository.GetOneAsync(user => user.Email == createUserDto.Email) is not null)
        {
            context.ModelState.AddModelError("User", "Email already exist.");
            ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
            {
                Status = HttpStatusCode.Conflict as int?
            };
            context.Result = new ConflictObjectResult(details);
            return;
        }

        await next();
    }
}