using System.Threading.Tasks;
using Tsi.Template.Domain.Common;

namespace Tsi.Template.Abstraction.Common
{
    public interface IPermissionService
    {
        Task<bool> AuthorizeAsync(Permission permission);
        Task RemovePermissionForRoleAsync(string permissionSystemName, string roleName);
        Task AddPermissionToRoleAsync(string permissionSystemName, string roleName);
    }
}
