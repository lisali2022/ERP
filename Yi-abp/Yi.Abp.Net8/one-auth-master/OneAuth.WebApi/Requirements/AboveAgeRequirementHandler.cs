using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace OneAuth.WebApi.Requirements;

public class AboveAgeRequirementHandler : IAuthorizationHandler
//: AuthorizationHandler<AboveAgeRequirement>
{
    //protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AboveAgeRequirement requirement)
    //{
    //    if (context.User.HasClaim(m => m.Type == "Age"))
    //    {
    //        var age = context.User.FindFirstValue("Age");
    //        if((int.TryParse(age, out var value) && value > requirement.MinAge))
    //        {
    //            context.Succeed(requirement);
    //        }
    //    }
    //    return Task.CompletedTask;
    //}
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements)
        {
            if (context.User.HasClaim(m => m.Type == "Age"))
            {
                var age = context.User.FindFirstValue("Age");
                if ((int.TryParse(age, out var value) && value > 18))
                {
                    context.Succeed(requirement);
                }
            }
        }
        return Task.CompletedTask;
    }
}
