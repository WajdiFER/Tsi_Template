using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Core.Attributes;
using System.Linq;
using Tsi.Template.ViewModels.Grh.Employee;
using System;

namespace Tsi.Template.Web.Factories.Grh
{
    [Injectable(typeof(IEmployeeModelFactory))]
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public EmployeeModelFactory(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        public async Task<CreateEmployeeRequest> PopulateDepartmentsSelectListAsync(CreateEmployeeRequest model)
        {
            model.Departments = new SelectList(
                (await _departmentService.GetAllAsync()).ToDictionary(dep => dep.Id, dep => $"{dep.Code} - {dep.Libelle}")
                , "Key"
                , "Value");

            return model;
        }

        public async Task<CreateEmployeeRequest> PrepareEmployeeCreateModelAsync(int id)
        {
            var employee = await _employeeService.GetEmployeebyIdAsync(id);

            if(employee is null)
            {
                throw new ArgumentException("Cannot be null! ", nameof(employee));
            } 

            var model = new CreateEmployeeRequest()
            { 
                DepartementId = employee.DepartementId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Cin = employee.Cin,
                Id = employee.Id
            };

            model = await PopulateDepartmentsSelectListAsync(model);

            return model;
        }

        public async Task<CreateEmployeeRequest> PrepareEmployeeViewModelAsync() 
            => await PopulateDepartmentsSelectListAsync(new CreateEmployeeRequest());
    }
}