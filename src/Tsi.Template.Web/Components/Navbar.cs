using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SageClient.Components
{ 
    public class Navbar: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SimpleModel
            {
                LastName = "Selmi",
                Name = "Abdelaziz"
            };
            return await Task.FromResult((IViewComponentResult)View(model));
        }
    }


    public class SimpleModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
