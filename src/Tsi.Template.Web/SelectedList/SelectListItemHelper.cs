using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tsi.Template.Abstraction.Grh;
using Tsi.Template.Core;

namespace Tsi.Template.Web.SelectedList
{
    public class SelectListItemHelper
    {
        
        public static SelectList GetDepartementList()
        {
            var dictionary = new Dictionary<int, string>();

            var departements = EngineContext.Current.Resolve<IDepartmentService>().GetAllAsync().GetAwaiter().GetResult();

            foreach (var item in departements)
            {
                dictionary.Add(item.Id,item.Code);
            }

            return new SelectList(dictionary, "Key", "Value");
        }
        

    }
}