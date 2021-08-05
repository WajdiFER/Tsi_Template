using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Catalog;
using Tsi.Template.Helpers.Catalog;
using Tsi.Template.ViewModels.Catalog;

namespace Tsi.Template.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = (await _productService.GetAllAsync()).ToViewModels();
             
            return View(products);
        }


        public async Task<IActionResult> Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            var product = model.ToProduct();

            await _productService.CreateProductAsync(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Product/Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
