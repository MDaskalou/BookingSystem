using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace BookingSystem.Application.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public string RequiredRole { get; }

    public RoleRequirement(string requiredRole)
    {
        RequiredRole = requiredRole;
    }
}