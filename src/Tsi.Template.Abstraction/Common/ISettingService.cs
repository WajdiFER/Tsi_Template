using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Domain.Common;

namespace Tsi.Template.Abstraction.Common
{
    public interface ISettingService
    { 
        Task<Setting> GetSettingByIdAsync(int settingId);
         
        Task DeleteSettingAsync(Setting setting); 
         
        Task<Setting> GetSettingAsync(string key);
         
        Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue = default);
         
        Task SetSettingAsync<T>(string key, T value);

        Task<IList<Setting>> GetAllSettingsAsync();

        Task<bool> SettingExistsAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();
         
        Task<T> LoadSettingAsync<T>() where T : ISettings, new();
         
        Task<ISettings> LoadSettingAsync(Type type);
         
        Task SaveSettingAsync<T>(T settings) where T : ISettings, new();
 
        Task SaveSettingAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector) where T : ISettings, new(); 
 
        Task DeleteSettingAsync<T>() where T : ISettings, new();
 
        Task DeleteSettingAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();
        string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector)
            where TSettings : ISettings, new();
    }
}
