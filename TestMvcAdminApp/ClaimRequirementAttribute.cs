using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

public class ClaimRequirementAttribute : TypeFilterAttribute {
    public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter)) {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}

public class ClaimRequirementFilter : IAuthorizationFilter {

    private readonly Claim _claim;

    public ClaimRequirementFilter(Claim claim) {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {

        var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);

        if (!hasClaim) {
            // Set the response code to 401.
            context.HttpContext.Response.StatusCode = 401;
            context.Result = new RedirectResult("~/Account/AuthorizeFailed");
        }
    }
}