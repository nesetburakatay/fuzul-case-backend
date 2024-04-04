using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using System.Net;

namespace API.ActionFilters
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new CustomResponseDTO<NoResponseDTO>().SetStatusCode(HttpStatusCode.BadRequest).SetData(new NoResponseDTO()).SetIsSuccsess(false);
                foreach (var item in context.ModelState)
                {
                    foreach (var erroritem in item.Value.Errors.ToList())
                        response.AddError(erroritem.ErrorMessage);

                }
                context.Result = new BadRequestObjectResult(response);
                return;
            }
            await next();
        }
    }
}
