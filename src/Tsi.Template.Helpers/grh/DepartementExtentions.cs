using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Domain.Grh;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Helpers.grh
{
    public static class DepartementExtentions
    {
        public static Departement ToDepartement(this DepartementViewModel model) => new()
        {
            Code = model.Code,
            Libelle = model.Libelle,
        };

        public static DepartementViewModel ToViewModel(this Departement departement) => new()
        {
            Id = departement.Id,
            Code = departement.Code,
            Libelle = departement.Libelle,           
        };

        public static IEnumerable<Departement> ToDepartements(this IEnumerable<DepartementViewModel> models)
            => models.Select(ToDepartement);

        public static IEnumerable<DepartementViewModel> ToViewModels(this IEnumerable<Departement> departements)
            => departements.Select(ToViewModel);
    }
}

