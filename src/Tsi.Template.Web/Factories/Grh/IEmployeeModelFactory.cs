using System.Threading.Tasks;
using Tsi.Template.ViewModels.Grh;
using Tsi.Template.ViewModels.Grh.Employee; 

namespace Tsi.Template.Web.Factories.Grh
{
    public interface IEmployeeModelFactory
    {
        Task<CreateEmployeeRequest> PrepareEmployeeViewModelAsync();

        Task<CreateEmployeeRequest> PrepareEmployeeCreateModelAsync(int id);

        Task<CreateEmployeeRequest> PopulateDepartmentsSelectListAsync(CreateEmployeeRequest model);
    }
}