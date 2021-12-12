using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace UserApi.Domain
{
    public class Validate : ActionFilterAttribute
    {
        public void OnActionExecuting(HttpActionContext context)
        {
            var errors = context.ModelState.Where(a => a.Value.Errors.Any()).ToList();

            if (errors.Count > 0)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

            base.OnActionExecuting(context);
        }
    }
}
