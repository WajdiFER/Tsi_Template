using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Helpers.grh;
using Tsi.Template.ViewModels.Grh;
using Tsi.Template.Web.Factories.Grh; 
using Tsi.Template.ViewModels.Grh.Employee;

namespace Tsi.Template.Web.Controllers
{
    public class EmployeeController : TsiBaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departementService;
        private readonly IEmployeeModelFactory _employeeModelFactory;

        public EmployeeController(IEmployeeService EmployeeService, IEmployeeModelFactory EmployeeModelFactory , IDepartmentService DepartementService)
        {
            _employeeService = EmployeeService;
            _employeeModelFactory = EmployeeModelFactory;
            _departementService = DepartementService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var employees = await _employeeService.GetAllWithIncludeAsync();

            return View(employees.ToViewModels());
        }

        public async Task<IActionResult> Create()
        {
            var model = await _employeeModelFactory.PrepareEmployeeViewModelAsync(); 
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeRequest model)
        {
            if (!ModelState.IsValid)
            {
                model = await _employeeModelFactory.PopulateDepartmentsSelectListAsync(model);

                return View(model);
            } 

            var employee = model.ToModel();

            await _employeeService.CreateEmployeeAsync(employee);

            return RedirectToAction(nameof(Index));
        } 

        [HttpGet("Employee/Edit/{id}")]
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _employeeModelFactory.PrepareEmployeeCreateModelAsync(id);
            await _employeeModelFactory.PopulateDepartmentsSelectListAsync(model);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditAsync(CreateEmployeeRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _employeeService.UpdateEmployeeAsync(model.Id, model.ToModel());
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Employee/Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
