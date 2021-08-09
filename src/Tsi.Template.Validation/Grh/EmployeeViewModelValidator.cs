using System.Text.RegularExpressions;
using FluentValidation;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Core;
using Tsi.Template.ViewModels.Grh.Employee;

namespace Tsi.Template.Validation.Grh
{
    public class EmployeeViewModelValidator : TsiBaseValidator<CreateEmployeeRequest>
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

            RuleFor(p => p.Cin)
                .Must(IsCinUnique)
                .WithMessage("Cin must be unique"); 

            RuleFor(p => p.DepartementId).Must(DepartementId =>
            {
                var departement = _departementService.GetDepartementbyId(DepartementId).GetAwaiter().GetResult();

                return departement is not null;
            }).WithMessage("Departement does not exist in the database");

            RuleFor(p => p.DepartementId)
                .NotEmpty()
                .WithMessage("Please enter department");
        }

        
        public bool IsCinUnique(CreateEmployeeRequest editedEmployee, long newValue)
        {
            var employeesWithCin = _employeeService.GetEmployeetbyCinAsync(editedEmployee.Cin).GetAwaiter().GetResult();

            return employeesWithCin is null?
                editedEmployee.Cin == newValue:
                employeesWithCin.Id == editedEmployee.Id || editedEmployee.Cin != newValue;  
        }

        public static bool IsCin8Digits(long cin)
        {
            Regex rx = new(CoreDefaults.RegexPatterns.EIGHT_DIGITS
                , RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(cin.ToString());
        }
    }
}
