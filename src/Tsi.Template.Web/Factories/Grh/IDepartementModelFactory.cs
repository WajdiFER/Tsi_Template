using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Web.Factories.Grh
{
    public interface IDepartementModelFactory
    {
        Task<DepartementViewModel> PrepareDepartementViewModelAsync();
    }
}
