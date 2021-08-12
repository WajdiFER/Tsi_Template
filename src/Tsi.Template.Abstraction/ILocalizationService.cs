using System.Threading.Tasks;

namespace Tsi.Template.Abstraction
{
    public interface ILocalizationService
    {
        Task<string> LoadResource(string key);
    }
}
