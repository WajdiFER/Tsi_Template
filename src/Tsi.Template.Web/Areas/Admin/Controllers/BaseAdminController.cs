using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsi.Template.Web.Filters;

namespace Tsi.Template.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
         
    }
}
