using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsi.Template.Web.Controllers
{
    [Authorize]
    public class TsiBaseController : Controller
    { 
        public IActionResult Check()
        {
            return View("~/Views/NotAuthorized.cshtml");
        }
    }


}
