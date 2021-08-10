using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Common;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Repository;

namespace Tsi.Template.Services.Common
{
    [Injectable(typeof(IPermissionService))]
    public class PermissionService : IPermissionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Permission> _permissionRepo;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<PermissionUserRoleMapping> _permissionUserRoleRepo;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserService _userService;

        public PermissionService(IHttpContextAccessor httpContextAccessor
            , IRepository<PermissionUserRoleMapping> permissionUserRoleRepo
            , IRepository<Permission> permissionRepo
            , IUserRegistrationService userRegistrationService
            , IRepository<UserRole> userRoleRepository
            , IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _permissionUserRoleRepo = permissionUserRoleRepo;
            _permissionRepo = permissionRepo;
            _userRegistrationService = userRegistrationService;
            _userRoleRepository = userRoleRepository;
            _userService = userService;
        }


        #region Utilities

        private async Task<(UserRole, Permission)> GetRoleAndIdentityCoupleAsync(string permissionSystemName, string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException("Cannot be empty", nameof(roleName));
            }

            if (string.IsNullOrWhiteSpace(permissionSystemName))
            {
                throw new ArgumentNullException("Cannot be empty", nameof(permissionSystemName));
            }

            var identityRole = await _userRoleRepository.GetAsync(r => r.Name.Equals(roleName));

            if (identityRole is null)
            {
                throw new ApplicationException($"Role not found: {roleName}");
            }

            var permission = await _permissionRepo.GetAsync(pr => pr.SystemName.Equals(permissionSystemName));

            if (permission is null)
            {
                throw new ApplicationException($"Permission not found: {permissionSystemName}");
            }

            return (identityRole, permission);
        }


        private async Task<bool> AuthorizeAsync(string permissionSystemName, User user)
        {
            if (user is null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(permissionSystemName))
            {
                return false;
            }

            var userroles = await _userService.GetRolesAsync(user);

            if (userroles is null || userroles.Count() == 0)
            {
                return false;
            }

            var hasRight = false;

            foreach (var roleName in userroles)
            {
                hasRight = await AuthorizeAsync(permissionSystemName, roleName);

                if (hasRight)
                {
                    break;
                }
            }

            return hasRight;
        }

        private async Task<bool> AuthorizeAsync(string permissionSystemName, string roleName)
        {
            var identityRole = _roleManager.Roles.Where(r => r.Name.Equals(roleName)).FirstOrDefault();

            if (identityRole is null)
            {
                return false;
            }

            var permission = await _permissionRepo.GetAsync(p => p.SystemName.Equals(permissionSystemName));

            if (permission is null)
            {
                return false;
            }

            var permissionRoleMapping = await _permissionUserRoleRepo.GetAsync(pr => pr.PermissionId == permission.Id && pr.UserRoleId == identityRole.Id);

            return permissionRoleMapping is not null;
        }

        #endregion

        #region Public Methods
        public async Task<bool> AuthorizeAsync(Permission permission)
        {
            var username = GetCurrentUserUsername();

            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            var user = await _userManager.FindByNameAsync(username);

            return await AuthorizeAsync(permission.SystemName, user);
        }

        public async Task RemovePermissionForRoleAsync(string permissionSystemName, string roleName)
        {
            var (identityRole, permission) = await GetRoleAndIdentityCoupleAsync(permissionSystemName, roleName);

            await _permissionUserRoleRepo.DeleteAsync(pr => pr.PermissionId == permission.Id && pr.UserRoleId == identityRole.Id);
        }

        public async Task AddPermissionToRoleAsync(string permissionSystemName, string roleName)
        {
            var (identityRole, permission) = await GetRoleAndIdentityCoupleAsync(permissionSystemName, roleName);

            await _permissionUserRoleRepo.AddAsync(new()
            {
                PermissionId = permission.Id,
                UserRoleId = identityRole.Id
            });
        }

        #endregion

    }
}
