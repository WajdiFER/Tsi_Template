using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Domain.Common;

namespace Tsi.Template.Abstraction.Common
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<bool> ChangeUserPasswordAsync(User user, string oldPassword, string newPassword); 
        Task CreateUserAsync(User user);
        Task<IEnumerable<UserRole>> GetRolesAsync(User user);
        Task AssignUserToRoleAsync(User user, string userRoleSystemName);
        Task AssignUserToRolesAsync(User user, IEnumerable<string> userRolesSystemNames);
        Task<bool> IsUserAdminAsync(User user);
    }
}
