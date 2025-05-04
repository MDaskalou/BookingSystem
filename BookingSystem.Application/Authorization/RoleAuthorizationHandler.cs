using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Application.Authorization;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        var roleClaim = context.User.FindFirst(ClaimTypes.Role);

        if (roleClaim != null && roleClaim.Value == requirement.RequiredRole)
        {
            context.Succeed(requirement);
        }
        
        foreach (var claim in context.User.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
        }
        return Task.CompletedTask;
    }

   
}