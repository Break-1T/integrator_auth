using Microsoft.AspNetCore.Identity;

namespace integrator_auth.database.Models;

public class IntegratorUserRole : IdentityUserRole<Guid>
{
    public virtual IntegratorRole Role { get; set; }
}
