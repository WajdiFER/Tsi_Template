 
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Common;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Core.Configuration;
using Tsi.Template.ViewModels.Common;

namespace Tsi.Template.Web.Factories.Common
{
    [Injectable(typeof(IAccountModelFactory))]
    public class AccountModelFactory : IAccountModelFactory
    {
        private readonly ISettingService _settingService;

        public AccountModelFactory(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<LoginModel> PrepareLoginModelAsync()
        {
            var userSettings = await _settingService.LoadSettingAsync<UserSettings>();

            return new LoginModel
            {
                UserNameEnabled = userSettings.UserNameEnabled
            };
        }

        public async Task<UserRegisterModel> PrepareRegisterModelAsync()
        {
            var userSettings = await _settingService.LoadSettingAsync<UserSettings>();

            return new UserRegisterModel
            {
                UserNameEnabled = userSettings.UserNameEnabled
            };
        }
    }

    public interface IAccountModelFactory
    {
        Task<LoginModel> PrepareLoginModelAsync();
        Task<UserRegisterModel> PrepareRegisterModelAsync();
    }
}
