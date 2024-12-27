using Microsoft.AspNetCore.Authorization;

namespace OneAuth.WebApi.Requirements;

public class AboveAgeRequirement(int minAge) : IAuthorizationRequirement
{
    public int MinAge { get; } = minAge;
}
