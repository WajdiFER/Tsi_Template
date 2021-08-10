using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Helpers.grh;
using Tsi.Template.ViewModels.Grh;
using Tsi.Template.Web.Factories.Grh;

namespace Tsi.Template.Web.Controllers
{

    [Authorize]
    public class DepartementController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IDepartementModelFactory _departementModelFactory;
        public DepartementController(IDepartmentService DepartmentService, IDepartementModelFactory DepartementModelFactory)
        {
            _departmentService = DepartmentService;
            _departementModelFactory = DepartementModelFactory;
        }
        public async Task<IActionResult> IndexAsync()
        { 
            var departments = await _departmentService.GetAllAsync();

            return View(departments.ToViewModels());
        }

        public async Task<IActionResult> Create()
        {
            var model = await _departementModelFactory.PrepareDepartementViewModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var departement = model.ToDepartement();

            await _departmentService.CreateDepartementAsync(departement);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Departement/Edit/{id}")]
        public async Task<IActionResult> EditAsync(int id) => View((await _departmentService.GetDepartementbyId(id)).ToViewModel());


        [HttpPost]
        public async Task<IActionResult> EditAsync(DepartementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _departmentService.UpdateDepartementAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Departement/Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _departmentService.DeleteDepartementAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
