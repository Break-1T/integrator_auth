using integrator_auth.database.Models;
using Microsoft.AspNetCore.Identity;

namespace integrator_auth.Models;

public class IntegratorUser : IdentityUser<Guid>
{
    public DateTime RecCreated { get; set; }
    public DateTime RecModified { get; set; }

    public virtual ICollection<IntegratorUserRole> UserRoles { get; set; }
}
