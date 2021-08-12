using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Common;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Core.Helpers;
using Tsi.Template.Domain.Common;
using Tsi.Template.Infrastructure.Repository;

namespace Tsi.Template.Services.Common
{
    [Injectable(typeof(ISettingService))]
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(IRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task DeleteSettingAsync(Setting setting)
        {
            await _settingRepository.DeleteAsync(setting);
        }

        #region Utilities

        protected virtual async Task<IDictionary<string, IList<Setting>>> GetAllSettingsDictionaryAsync()
        {
            var settings = await GetAllSettingsAsync();

            var dictionary = new Dictionary<string, IList<Setting>>();
            foreach (var s in settings)
            {
                var resourceName = s.Name.ToLowerInvariant();
                var settingForCaching = new Setting
                {
                    Id = s.Id,
                    Name = s.Name,
                    Value = s.Value
                };
                if (!dictionary.ContainsKey(resourceName))
                {
                    dictionary.Add(resourceName, new List<Setting>
                        {
                            settingForCaching
                        });
                }
            }

            return dictionary;
        }

        protected virtual async Task SetSettingAsync(Type type, string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

            var allSettings = await GetAllSettingsDictionaryAsync();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //update
                var setting = await GetSettingByIdAsync(settingForCaching.Id);
                setting.Value = valueStr;
                await UpdateSettingAsync(setting);
            }
            else
            {
                //insert
                var setting = new Setting
                {
                    Name = key,
                    Value = valueStr
                };
                await InsertSettingAsync(setting);
            }
        }

        public virtual async Task InsertSettingAsync(Setting setting)
        {
            await _settingRepository.AddAsync(setting);
        }

        public virtual async Task UpdateSettingAsync(Setting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            await _settingRepository.UpdateAsync(setting);
        }

        public virtual async Task DeleteSettingsAsync(IList<Setting> settings)
        {
            foreach (var item in settings)
            {
                await _settingRepository.DeleteAsync(item);
            }
        }

        #endregion

        public async Task DeleteSettingAsync<T>() where T : ISettings, new()
        {
            var settingsToDelete = new List<Setting>();
            var allSettings = await GetAllSettingsAsync();
            foreach (var prop in typeof(T).GetProperties())
            {
                var key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            await DeleteSettingsAsync(settingsToDelete);
        }

        public async Task DeleteSettingAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var key = GetSettingKey(settings, keySelector);
            key = key.Trim().ToLowerInvariant();

            var allSettings = await GetAllSettingsDictionaryAsync();

            if (allSettings.TryGetValue(key, out var settingToDelete) && settingToDelete.Count > 0)
            {
                var setting = await GetSettingByIdAsync(settingToDelete.FirstOrDefault().Id);
                await DeleteSettingAsync(setting);
            }
        }

        public async Task<IList<Setting>> GetAllSettingsAsync()
        {
            return (await _settingRepository.GetAllAsync(orderBy: s => s.Name)).ToList();
        }

        public async Task<Setting> GetSettingAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            key = key.Trim().ToLowerInvariant();
            return await _settingRepository.GetAsync(setting => setting.Name.Equals(key));
        }

        public async Task<Setting> GetSettingByIdAsync(int settingId)
        {
            return await _settingRepository.GetByIdAsync(settingId);
        }

        public async Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue = default)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            var settings = await GetAllSettingsDictionaryAsync();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
            {
                return defaultValue;
            }

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault();


            return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
        }

        public async Task<T> LoadSettingAsync<T>() where T : ISettings, new()
        {
            return (T)await LoadSettingAsync(typeof(T));
        }

        public async Task<ISettings> LoadSettingAsync(Type type)
        {
            var settings = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                {
                    continue;
                }

                var key = type.Name + "." + prop.Name;
                //load by store
                var setting = await GetSettingByKeyAsync<string>(key);
                if (setting == null)
                {
                    continue;
                }

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                {
                    continue;
                }

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                {
                    continue;
                }

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings as ISettings;
        }

        public async Task SaveSettingAsync<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                {
                    continue;
                }

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                {
                    continue;
                }

                var key = typeof(T).Name + "." + prop.Name;
                var value = prop.GetValue(settings, null);
                if (value != null)
                {
                    await SetSettingAsync(prop.PropertyType, key, value);
                }
                else
                {
                    await SetSettingAsync(key, string.Empty);
                }
            }

        }

        public async Task SaveSettingAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            if (keySelector.Body is not MemberExpression member)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");
            }

            var key = GetSettingKey(settings, keySelector);
            var value = (TPropType)propInfo.GetValue(settings, null);
            if (value != null)
            {
                await SetSettingAsync(key, value);
            }
            else
            {
                await SetSettingAsync(key, string.Empty);
            }
        }

        public async Task SetSettingAsync<T>(string key, T value)
        {
            await SetSettingAsync(typeof(T), key, value);
        }

        public async Task<bool> SettingExistsAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var key = GetSettingKey(settings, keySelector);

            var setting = await GetSettingByKeyAsync<string>(key);
            return setting != null;
        }

        public virtual string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector)
            where TSettings : ISettings, new()
        {
            if (keySelector.Body is not MemberExpression member)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");
            }

            if (member.Member is not PropertyInfo propInfo)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");
            }

            var key = $"{typeof(TSettings).Name}.{propInfo.Name}";

            return key;
        }
    }
}
