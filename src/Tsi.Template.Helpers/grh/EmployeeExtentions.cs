using System.Collections.Generic;
using System.Linq;
using Tsi.Template.Domain.Grh;
using Tsi.Template.ViewModels.Grh;
using Tsi.Template.ViewModels.Grh.Employee;

namespace Tsi.Template.Helpers.grh
{
    public static class EmployeeExtentions
    {
        public static Employee ToEmployee(this EmployeeViewModel model) => new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Cin = model.Cin
        };

        public static EmployeeViewModel ToViewModel(this Employee employee) => new()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Cin = employee.Cin,
            DepartementName = employee.Departement.Libelle
        };

        public static IEnumerable<Employee> ToEmployees(this IEnumerable<EmployeeViewModel> models)
            => models.Select(ToEmployee);

        public static IEnumerable<EmployeeViewModel> ToViewModels(this IEnumerable<Employee> employees)
            => employees.Select(ToViewModel);


        public static Employee ToModel(this CreateEmployeeRequest model) => new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Cin = model.Cin,
            DepartementId = model.DepartementId,
            Id = model.Id
        };
    }
}
