using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Domain.Common;

namespace Tsi.Template.Abstraction.Common
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string systemName, string Name);
        Task CreateRoleAsync(string systemName);
        Task UpdateRoleAsync(int id, string systemName, string name);
        Task<IEnumerable<UserRole>> GetAllRoles();
    }
}
