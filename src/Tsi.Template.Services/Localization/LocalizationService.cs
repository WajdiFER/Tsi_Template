using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Abstraction;
using Tsi.Template.Core.Attributes;

namespace Tsi.Template.Services.Localization
{
    [Injectable(typeof(ILocalizationService))]
    public class LocalizationService : ILocalizationService
    {
        public Task<string> LoadResource(string key)
        {
            return Task.FromResult($"{key} - this message comes from the implemented localization service");
        }
    }
}
