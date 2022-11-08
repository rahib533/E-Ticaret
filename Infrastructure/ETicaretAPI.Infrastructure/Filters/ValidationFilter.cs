using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ETicaretAPI.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Where(m=>m.Value.Errors.Any())
                .ToDictionary(k => k.Key, v=>v.Value.Errors.Select(s=>s.ErrorMessage)).ToList();
                context.Result = new BadRequestObjectResult(errors);
                return;
            }
            await next();
        }
    }
}