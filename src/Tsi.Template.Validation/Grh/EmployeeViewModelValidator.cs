using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Domain.Grh;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Validation.Grh
{
    public class EmployeeViewModelValidator : TsiBaseValidator<EmployeeViewModel>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departementService;

        public EmployeeViewModelValidator(IEmployeeService EmployeeService, IDepartmentService DepartementService)
        {
            _employeeService = EmployeeService;
            _departementService = DepartementService;

            RuleFor(p => p.FirstName)
                .NotNull().NotEmpty()
                .WithMessage("First Name canoot be empty");

            RuleFor(p => p.LastName)
                .NotNull().NotEmpty()
                .WithMessage("Last Name canoot be empty");

            RuleFor(p => p.Cin)
                .Must(IsCin8Digits)
                .WithMessage("Cin must be with 8 digits");

            RuleFor(p => p.Cin).Must(IsCinUnique)
                .WithMessage("Cin must be unique");




            RuleFor(p => p.DepartementId).Must(DepartementId =>
            {
                var departement = _departementService.GetDepartementbyId(DepartementId).GetAwaiter().GetResult();

                return departement is not null;
            }).WithMessage("Departement does not exist in the database");




        }

        
        public bool IsCinUnique(EmployeeViewModel editedEmployee, long newValue)
        {
            var employeesWithCin = _employeeService.GetEmployeetbyCinAsync(editedEmployee.Cin).GetAwaiter().GetResult();
            if(employeesWithCin == null)
                return editedEmployee.Cin == newValue;
            else
            {
                return employeesWithCin.Id == editedEmployee.Id || editedEmployee.Cin != newValue;
            }
            
        }

        public bool IsCin8Digits(long cin)
        {
            Regex rx = new Regex(@"^\d{8}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rx.IsMatch(cin.ToString());
        }
    }
}
