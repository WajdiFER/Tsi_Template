using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Catalog;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Events;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.Web.Models;

namespace Tsi.Template.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
         

        public async Task<IActionResult> Index()
        { 

            var product = new Product
            {
                Code = "1",
                Libelle = "My first product",
                Price = 25.45M
            };

            await _productService.CreateProductAsync(product);

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await _productService.DeleteProductAsync(1);

            return View();
        }


        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();

            return Ok(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
