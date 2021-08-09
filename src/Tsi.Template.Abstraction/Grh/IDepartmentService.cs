using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Domain.Grh;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Abstraction.Grh
{
    public interface IDepartmentService
    {
        Task<Departement> CreateDepartementAsync(Departement departement);
        Task DeleteDepartementAsync(int id);
        Task<IEnumerable<Departement>> GetAllAsync();
        Task<Departement> GetDepartementbyId(int id);
        Task<Departement> GetDepartementbyCode(string code);
        Task UpdateDepartementAsync(DepartementViewModel model);
    }
}
