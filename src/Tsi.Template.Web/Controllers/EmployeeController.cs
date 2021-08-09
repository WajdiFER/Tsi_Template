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

namespace Tsi.Template.Web.Controllers
{
    public class EmployeeController : Controller
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
        //    ViewBag.departemetsId = new SelectList(await _departementService.GetAllAsync(), "Id", "Name" ); ;
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }
         //   ViewBag.departemetsId = _departementService.GetAllAsync().GetAwaiter().GetResult().Select(t => t.Id).ToList();
            var employee = model.ToEmployee();

            await _employeeService.CreateEmployeeAsync(employee);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Employee/Edit/{id}")]
        public async Task<IActionResult> EditAsync(int id) => View((await _employeeService.GetEmployeebyIdAsync(id)).ToViewModel());


        [HttpPost]
        public async Task<IActionResult> EditAsync(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _employeeService.UpdateEmployeeAsync(model);
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
