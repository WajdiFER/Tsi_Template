using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Domain.Grh;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Abstraction.Grh
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetEmployeebyIdAsync(int id);
        Task<Employee> GetEmployeetbyCinAsync(long cin);
        Task UpdateEmployeeAsync(int id, Employee model);
        Task<IEnumerable<Employee>> GetAllWithIncludeAsync();
    }
}