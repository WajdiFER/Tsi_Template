using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsi.Template.Core.Attributes;
using Tsi.Template.ViewModels.Grh;

namespace Tsi.Template.Web.Factories.Grh
{
    [Injectable(typeof(IDepartementModelFactory))]
    public class DepartementModelFactory : IDepartementModelFactory
    {
        public Task<DepartementViewModel> PrepareDepartementViewModelAsync() => Task.FromResult(new DepartementViewModel());
    }
}
