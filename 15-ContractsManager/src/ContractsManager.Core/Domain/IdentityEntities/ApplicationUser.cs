using Microsoft.AspNetCore.Identity;

namespace ContractsManager.Core.Domain.IdentityEntities;

public class ApplicationUser:IdentityUser<Guid>
{
    public string? PersonName { get; set; }
}

public class ApplicationRole : IdentityRole<Guid>
{
    
}