using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Common;
using Tsi.Template.Core;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Common;
using Tsi.Template.Domain.Defaults;
using Tsi.Template.Infrastructure.Repository;

namespace Tsi.Template.Services.Common
{
    [Injectable(typeof(IUserService))]
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRepository<User> _userRepository; 
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<UserRoleMapping> _userRoleMappingRepository;

        public UserService(IPasswordHasher passwordHasher
            , IRepository<User> userRepository
            , IRepository<UserRole> userRoleRepository
            , IRepository<UserRoleMapping> userRoleMappingRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _userRoleMappingRepository = userRoleMappingRepository;
        }

        public async Task<bool> ChangeUserPasswordAsync(User user, string oldPassword, string newPassword)
        {
            var saltedOldPassword = _passwordHasher.GenerateHash(oldPassword, user.Salt);

            if (!user.Password.Equals(saltedOldPassword))
            {
                // wrong old password
                return false;
            }

            //generate new salt
            var newSalt = _passwordHasher.GenerateSalt();
            var newPasswordHash = _passwordHasher.GenerateHash(newPassword, newSalt);

            user.Salt = newSalt;
            user.Password = newPasswordHash;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var normalizedEmail = email.Trim().ToUpper();
            return await _userRepository.GetAsync(u => u.NormalizedEmail.Equals(email));
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            var normalizedUserName = username.Trim().ToUpper();
            return await _userRepository.GetAsync(u => u.NormalizedUsername.Equals(normalizedUserName));
        }

        public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return null;
            }

            var normalizedUserName = phoneNumber.Trim().ToUpper();
            return await _userRepository.GetAsync(u => u.PhoneNumber.Equals(normalizedUserName));
        }

        public async Task CreateUserAsync(User user)
        {
            if (user is null)
            {
                return;
            }

            await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<UserRole>> GetRolesAsync(User user)
        {
            if(user is null)
            {
                return null;
            }

            var userRoleMappings = await _userRoleMappingRepository.GetManyAsync(userRoleMapping => userRoleMapping.UserId == user.Id);

            if (!userRoleMappings.Any())
            {
                return null;
            }

            var result = new List<UserRole>();

            foreach (var userRoleMapping in userRoleMappings)
            {
                var userRole = await _userRoleRepository.GetAsync(userRole => userRole.Id == userRoleMapping.UserRoleId);

                if(userRole is not null)
                {
                    result.Add(userRole);
                } 
            }

            return result;
        }

        public async Task AssignUserToRoleAsync(User user, string userRoleSystemName)
        {
            if (string.IsNullOrWhiteSpace(userRoleSystemName))
            {
                throw new ArgumentNullException("Cannot be empty", nameof(userRoleSystemName));
            }

            var userRole = await _userRoleRepository.GetAsync(userRole => userRole.SystemName.Equals(userRoleSystemName));

            if(userRole is null)
            {
                throw new ApplicationException($"Role with system name ({userRoleSystemName}) not found.");
            }

            var userRoleMapping = await _userRoleMappingRepository.GetAsync(userRoleMapping => userRoleMapping.UserRoleId == userRole.Id && userRoleMapping.UserId == user.Id);

            if(userRoleMapping is not null)
            {
                return;
            }

            await _userRoleMappingRepository.AddAsync(new()
            {
                UserId = user.Id,
                UserRoleId = userRole.Id
            });
        }

        public async Task AssignUserToRolesAsync(User user, IEnumerable<string> userRolesSystemNames)
        {
            await _userRoleMappingRepository.DeleteAsync(userRoleMapping => userRoleMapping.UserId == user.Id);

            foreach (var userRole in userRolesSystemNames)
            { 
                await AssignUserToRoleAsync(user, userRole);
            }
        }

        public async Task<bool> IsUserAdminAsync(User user)
        {
            if(user is null)
            {
                return false;
            }

            var adminRole = await _userRoleRepository.GetAsync(role => role.SystemName.Equals(CoreDefaults.AdministratorRole));

            if(adminRole is null)
            {
                // we make sure that the admin role is created
                await _userRoleRepository.AddAsync(new()
                {
                    SystemName = CoreDefaults.AdministratorRole,
                    Name = "Administrator"
                });

                return false;
            }

            foreach (var item in await GetRolesAsync(user))
            {
                if (item.SystemName.Equals(CoreDefaults.AdministratorRole))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
