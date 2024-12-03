namespace integrator_auth.database.Constants;

public static class DbConstants
{
    public static Guid MasterAdminUserId = Guid.Parse("EDC610EA-54E0-4C8F-87D1-103B4F341B6B");

    /// <summary>
    /// The user role name.
    /// </summary>
    public const string UserRoleName = "User";

    /// <summary>
    /// The user role name.
    /// </summary>
    public const string DeveloperRoleName = "Developer";

    /// <summary>
    /// The user role name.
    /// </summary>
    public const string AdminRoleName = "Admin";

    /// <summary>
    /// The master admin role name.
    /// </summary>
    public const string MasterAdminRoleName = "MasterAdmin";

    /// <summary>
    /// The user role identifier.
    /// </summary>
    public const string UserRoleId = "EB230FF7-64E2-480C-828B-C00B6917EFB5";

    /// <summary>
    /// The developer role identifier
    /// </summary>
    public const string DeveloperRoleId = "CDA06594-FE5E-40AA-A07B-A5C568C3D33D";
    
    /// <summary>
    /// The admin role identifier
    /// </summary>
    public const string AdminRoleId = "9C90838E-DD8C-4A5D-92AB-FD8F42735A96";

    /// <summary>
    /// The master admin role identifier.
    /// </summary>
    public const string MasterAdminRoleId = "9268D8A2-7B04-4BC4-88D9-5CA9643D4C3F";

    /// <summary>
    /// The roles.
    /// </summary>
    public static Dictionary<string, Guid> Roles = new Dictionary<string, Guid>
    {
        { UserRoleName, Guid.Parse(UserRoleId) },
        { DeveloperRoleName, Guid.Parse(DeveloperRoleId) },
        { AdminRoleName, Guid.Parse(AdminRoleId) },
        { MasterAdminRoleName, Guid.Parse(MasterAdminRoleId) },
    };
}
