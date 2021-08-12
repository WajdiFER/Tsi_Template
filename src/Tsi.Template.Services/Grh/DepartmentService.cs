using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Grh;
using Tsi.Template.Infrastructure.Repository;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Services.Grh
{
    [Injectable(typeof(IDepartmentService))]
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Departement> _departementRepo;

        public DepartmentService(IRepository<Departement> repository)
        {
            _departementRepo = repository;
        }
        public async Task<Departement> CreateDepartementAsync(Departement departement)
        {
            return await _departementRepo.AddAsync(departement);
        }

        public async Task DeleteDepartementAsync(int id)
        {
            await _departementRepo.DeleteAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Departement>> GetAllAsync()
        {
            return await _departementRepo.GetAllAsync();
        }

        public async Task<Departement> GetDepartementbyCode(string code)
        {
            return await _departementRepo.GetAsync(t => t.Code.Equals(code));
        }

        public async Task<Departement> GetDepartementbyId(int id)
        {
            return await _departementRepo.GetByIdAsync(id);
        }

        public async Task UpdateDepartementAsync(DepartementViewModel model)
        {
            var departement = await GetDepartementbyId(model.Id);

            if (departement is null)
            {
                return;
            }

            departement.Code = model.Code;
            departement.Libelle = model.Libelle;

            await _departementRepo.UpdateAsync(departement);
        }
    }
}
