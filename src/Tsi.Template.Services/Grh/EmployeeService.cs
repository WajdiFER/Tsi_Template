using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Grh;
using Tsi.Template.Infrastructure.Repository;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Services.Grh
{
    [Injectable(typeof(IEmployeeService))]
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;
        public EmployeeService(IRepository<Employee> EmployeeRepo)
        {
            _employeeRepo = EmployeeRepo;
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            return await _employeeRepo.AddAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepo.DeleteAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllWithIncludeAsync()
        {
            return await _employeeRepo.GetManyWithIncludeAsync(null, null, t => t.Departement);
        }

        public async Task<Employee> GetEmployeebyIdAsync(int id)
        {
            return await _employeeRepo.GetByIdAsync(id);
        }

        public async Task<Employee> GetEmployeetbyCinAsync(long cin)
        {
            return await _employeeRepo.GetAsync(t => t.Cin == cin);
        }

        public async Task UpdateEmployeeAsync(int id, Employee model)
        {
            var employee = await GetEmployeebyIdAsync(id);

            if (employee is null)
            {
                return;
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Cin = model.Cin;
            employee.DepartementId = model.DepartementId; 

            await _employeeRepo.UpdateAsync(employee);
        }
    }
}