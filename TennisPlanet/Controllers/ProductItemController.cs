using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data.Domain;
using TennisPlanet.Models.Brand;
using TennisPlanet.Models.Category;
using TennisPlanet.Models.ProductItem;

namespace TennisPlanet.Controllers
{
    [Authorize(Roles = "Administrator")]
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
        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductItemIndexVM> products = _productItemService.GetProductItems(searchStringCategoryName, searchStringBrandName)
                .Select(products => new ProductItemIndexVM
                {
                    Id = products.Id,
                    ItemName = products.ItemName,
                    BrandId = products.BrandId,
                    BrandName = products.Brand.BrandName,
                    CategoryId = products.CategoryId,
                    CategoryName = products.Category.CategoryName,
                    Picture = products.Picture,
                    Description = products.Description,
                    Price = products.Price,
                    Discount = products.Discount,
                }).ToList();
            return this.View(products);
        }
        [AllowAnonymous]
        // GET: ProductItemController/Details/5
        public ActionResult Details(int id)
        {
            ProductItem item = _productItemService.GetProductItemById(id);
            if (item == null)
            {
                return NotFound();
            }   
            ProductItemDetailsVM productItem = new ProductItemDetailsVM()
            {
                Id = item.Id,
                ItemName = item.ItemName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.Id,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Description = item.Description,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(productItem);
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
            productItem.Categories = _categoryService.GetCategories()
                .Select(x => new CategoryPairVM()
                {
                    Id = x.Id,
                    Name = x.CategoryName
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
                var createdId = _productItemService.Create(productItem.ItemName, productItem.BrandId,
                    productItem.CategoryId, productItem.Picture, productItem.Description, productItem.Price, productItem.Discount);
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
            ProductItem productItem = _productItemService.GetProductItemById(id);
            if (productItem == null)
            {
                return NotFound();
            }

            ProductItemEditVM updatedProductItem = new ProductItemEditVM()
            {
                Id = productItem.Id,
                ItemName = productItem.ItemName,
                BrandId = productItem.BrandId,
                CategoryId = productItem.CategoryId,
                Picture = productItem.Picture,
                Description = productItem.Description,
                Price = productItem.Price,
                Discount = productItem.Discount
            };
            updatedProductItem.Brands = _brandService.GetBrands()
                .Select(b => new BrandPairVM()
                {
                    Id = b.Id,
                    Name = b.BrandName
                }).ToList();
            updatedProductItem.Categories = _categoryService.GetCategories()
                .Select(c => new CategoryPairVM()
                {
                    Id = c.Id,
                    Name = c.CategoryName
                }).ToList();
            return View(updatedProductItem);
        }

        // POST: ProductItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductItemEditVM productItem)
        {
            if (ModelState.IsValid)
            {
                var updated = _productItemService.Update(id,productItem.ItemName, productItem.BrandId, productItem.CategoryId, 
                    productItem.Picture, productItem.Description, productItem.Price, productItem.Discount);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(productItem);
        }

        // GET: ProductItemController/Delete/5
        public ActionResult Delete(int id)
        {
            ProductItem item = _productItemService.GetProductItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductItemDeleteVM productItem = new ProductItemDeleteVM()
            {
                Id = item.Id,
                ItemName = item.ItemName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.Id,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Description = item.Description,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(productItem);
        }

        // POST: ProductItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productItemService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
