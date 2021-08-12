using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Catalog;
using Tsi.Template.Helpers.Catalog;
using Tsi.Template.ViewModels.Catalog;
using Tsi.Template.Web.Factories.Catalog;

namespace Tsi.Template.Web.Controllers
{
    public class ProductController : TsiBaseController
    {
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;

        public ProductController(IProductService productService, IProductModelFactory productModelFactory)
        {
            _productService = productService;
            _productModelFactory = productModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var products = (await _productService.GetAllAsync()).ToViewModels();
            
            return View(products);
        }


        public async Task<IActionResult> Create()
        {
            var model = await _productModelFactory.PrepareProductViewModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

        [HttpGet("Product/Edit/{id}")]
        public async Task<IActionResult> EditAsync(int id) => View((await _productService.GetProductbyId(id)).ToViewModel());


        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await  _productService.UpdateProductAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
