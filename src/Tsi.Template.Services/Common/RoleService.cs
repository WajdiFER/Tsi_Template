using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Common;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Repository;

namespace Tsi.Template.Services.Common
{
    [Injectable(typeof(IRoleService))]
    public class RoleService : IRoleService
    {
        private readonly IRepository<UserRole> _userRoleRepository;

        public RoleService(IRepository<UserRole> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task CreateRoleAsync(string systemName)
        {
            await CreateRoleAsync(systemName, systemName);
        }

        public async Task CreateRoleAsync(string systemName, string Name)
        {
            if (string.IsNullOrWhiteSpace(systemName))
            {
                throw new ApplicationException("System name should not be empty");
            }

            systemName = systemName.Trim();

            var roleExist = await _userRoleRepository.GetAsync(userRole => userRole.SystemName.Equals(systemName));

            if(roleExist is not null)
            {
                throw new ApplicationException("Role already exists in database");
            }

            await _userRoleRepository.AddAsync(new()
            {
                SystemName = systemName,
                Name = Name
            });
        }

        public async Task<IEnumerable<UserRole>> GetAllRoles()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public async Task UpdateRoleAsync(int id, string systemName, string name)
        {
            var roleExist = await _userRoleRepository.GetAsync(userRole => userRole.Id.Equals(id));

            if (roleExist is not null)
            {
                throw new ApplicationException("Role already exists in database");
            }

            if (string.IsNullOrWhiteSpace(systemName))
            {
                throw new ApplicationException("System name should not be empty");
            }

            roleExist.SystemName = systemName.Trim();
            roleExist.Name = name;

            await _userRoleRepository.UpdateAsync(roleExist);
        }
    }
}
