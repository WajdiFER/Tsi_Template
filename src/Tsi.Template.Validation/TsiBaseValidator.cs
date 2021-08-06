using FluentValidation;
using System.Threading.Tasks;
using Tsi.Template.Abstraction;
using Tsi.Template.Core;

namespace Tsi.Template.Validation
{
    public class TsiBaseValidator<TValidator> : AbstractValidator<TValidator>
    {
        private readonly ILocalizationService _localizationService;

        public TsiBaseValidator()
        {
            _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        }

        protected string GetMessage(string key)
        {
            if (_localizationService is null)
            {
                return key;
            }
            return _localizationService.LoadResource(key).GetAwaiter().GetResult();
        }
    }
}
