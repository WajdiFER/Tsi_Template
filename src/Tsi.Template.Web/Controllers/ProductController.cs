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

        // Retrieve product from database
        // Show form with the product info
        [HttpGet("Product/Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            // retrieve product from database
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return RedirectToAction(nameof(Index));
            }

            // map product to view model
            var model = product.ToViewModel();

            // return view(model)
            return View(model);
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateAsync(ProductViewModel model)
        {
            await _productService.UpdateProductAsync(model.Id, model);

            return RedirectToAction(nameof(Index));
        }

    }
}
