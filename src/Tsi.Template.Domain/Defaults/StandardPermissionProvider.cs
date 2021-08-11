using Tsi.Template.Domain.Common;

namespace Tsi.Template.Domain.Defaults
{
    public static class StandardPermissionProvider
    {
        public static Permission ManageDepartements => new()
        {
            SystemName = "Managae_Department",
            Name = "Permissions.ManageDepartments"
        };

        public static readonly Permission AccessAdminPanel = new Permission { Name = "Access admin area", SystemName = "AccessAdminPanel"};
    }
}
