using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;

public class ClaimRequirementAttribute : ActionFilterAttribute, IAuthorizationFilter {
    public void OnAuthorization(AuthorizationFilterContext context) {

        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var claimName = descriptor.ControllerName + "-" + descriptor.ActionName;
        var claim = new Claim(claimName, "True");
        var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == claim.Type && c.Value == claim.Value);

        if (!hasClaim)
        {
            // Set the response code to 401.
            context.HttpContext.Response.StatusCode = 401;
            context.Result = new RedirectResult("~/account/authorize-failed");
        }
    }
}
