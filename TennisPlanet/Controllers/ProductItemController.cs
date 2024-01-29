using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data.Domain;
using TennisPlanet.Models.Brand;
using TennisPlanet.Models.ProductItem;

namespace TennisPlanet.Controllers
{
    public class ProductItemController : Controller
    {
        private readonly IProductItemService _productItemService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductItemController(IProductItemService productItemService, ICategoryService categoryService, IBrandService brandService)
        {
            _productItemService = productItemService;
            _categoryService = categoryService;
            _brandService = brandService;
        }
        // GET: ProductItemController
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            return View();
        }

        // GET: ProductItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductItemController/Create
        public ActionResult Create()
        {
            var productItem = new ProductItemCreateVM();
            productItem.Brands = _brandService.GetBrands()
                .Select(x => new BrandPairVM()
                {
                    Id = x.Id,
                    Name = x.BrandName
                }).ToList();
            return View(productItem);
        }

        // POST: ProductItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProductItemCreateVM productItem)
        {
            if (ModelState.IsValid)
            {
                var createdId = _productItemService.Create(productItem.ProductName, productItem.BrandId,
                    productItem.CategoryId, productItem.Picture, productItem.Price, productItem.Discount);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: ProductItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
