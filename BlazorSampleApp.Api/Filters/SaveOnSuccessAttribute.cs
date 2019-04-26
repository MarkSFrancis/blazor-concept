using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BlazorSampleApp.Api;

namespace BlazorSampleApp.Api.Filters
{
    public class SaveOnSuccessAttribute : TypeFilterAttribute
    {
        public SaveOnSuccessAttribute() : base(typeof(SaveOnSuccessImplementation))
        {
        }

        private class SaveOnSuccessImplementation : IActionFilter
        {
            public SaveOnSuccessImplementation(BlazorDbContext context)
            {
                Context = context;
            }

            protected BlazorDbContext Context { get; }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                var statusCode = context.HttpContext.Response.StatusCode;

                if (statusCode >= 200 && statusCode < 300)
                {
                    // Success - save changes
                    Context.SaveChanges();
                }
            }

            public void OnActionExecuting(ActionExecutingContext context)
            { }
        }
    }
}
