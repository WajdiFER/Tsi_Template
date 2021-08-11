using System.Threading.Tasks;
using Tsi.Template.Core.Enums;
using Tsi.Template.Domain.Common;
using Tsi.Template.ViewModels.Common;

namespace Tsi.Template.Abstraction.Common
{
    public interface IUserRegistrationService
    {
        Task<RegisterResult> RegisterUser(UserRegisterModel model);
        Task SignInUserAsync(User user, bool isPersistent = false);
        Task SignOutAsync();
        Task<LoginResult> ValidateUser(LoginModel model);

        Task<User> GetCurrentUserAsync();
    }
}
