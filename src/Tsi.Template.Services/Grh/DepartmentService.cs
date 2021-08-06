using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Grh;
using Tsi.Template.Infrastructure.Repository;

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

        public Task DeleteDepartementAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Departement>> GetAllAsync()
        {
            return await _departementRepo.GetAllAsync();
        }

        public Task<Departement> GetDepartementbyCode(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Departement> GetDepartementbyId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
