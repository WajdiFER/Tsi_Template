using System.Threading.Tasks;
using Tsi.Template.Core.Attributes;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Web.Factories.Grh
{
    [Injectable(typeof(IEmployeeModelFactory))]
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        public Task<EmployeeViewModel> PrepareEmployeeViewModelAsync() => Task.FromResult(new EmployeeViewModel());
    }
}