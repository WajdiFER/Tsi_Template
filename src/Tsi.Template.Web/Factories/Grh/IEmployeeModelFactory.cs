using System.Threading.Tasks;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Web.Factories.Grh
{
    public interface IEmployeeModelFactory
    {
        Task<EmployeeViewModel> PrepareEmployeeViewModelAsync();
    }
}